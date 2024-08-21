using AutoMapper;
using System.Data;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using AdminGestaoApi.Domain.Entity.Usuario;
using AdminGestaoApi.Infra.CrossCutting.Logging.Dto;
using AdminGestaoApi.Domain.Services.Interfaces.Usuario;
using AdminGestaoApi.ApplicationService.Requests.Usuario;
using AdminGestaoApi.ApplicationService.Services.Password;
using AdminGestaoApi.ApplicationService.Responses.Usuario;
using AdminGestaoApi.Infra.CrossCutting.Logging.Interfaces;
using AdminGestaoApi.ApplicationService.Services.Interfaces.Data;
using AdminGestaoApi.ApplicationService.Services.Interfaces.Usuario;

namespace AdminGestaoApi.ApplicationService.Services.Usuario
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;
        private readonly ILoggingProvider _loggingProvider;
        private readonly IMySqlConnection _mySqlConnection;
        private const int USUARIO_DELETADO = 1;

        public UsuarioApplicationService(ILoggingProvider loggingProvider, IUsuarioService usuarioService, IMapper mapper, IMySqlConnection mySqlConnection)
        {
            _mapper = mapper;
            _usuarioService = usuarioService;
            _loggingProvider = loggingProvider;
            _mySqlConnection = mySqlConnection;
        }

        public async Task<UsuarioResponse?> Insert(UsuarioRequestInsercao usuario)
        {
            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.Insert - Iniciando inserção de usuário."));

            using IDbConnection? conexao = _mySqlConnection.AbrirConexao();

            if (conexao is null)
                return default;

            usuario.Password = PasswordHasher.HashPassword(usuario.Password);

            var usuarioInserido = await _usuarioService.Insert(conexao, _mapper.Map<UsuarioEntity>(usuario));

            if (usuarioInserido is null)
            {
                _loggingProvider.Erro(new LogDto("UsuarioApplicationService.Insert - Usuário não inserido."));
                return default;
            }

            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.Insert - Usuário inserido com sucesso."));
            return _mapper.Map<UsuarioResponse>(usuarioInserido);
        }

        public async Task<string?> Login(string? username, string? password)
        {
            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.Login - Iniciando login do usuário."));

            using IDbConnection? conexao = _mySqlConnection.AbrirConexao();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || conexao is null)
                return default;

            var usuarioBuscado = await _usuarioService.GetByUsername(conexao, username);

            if (usuarioBuscado is null || !usuarioBuscado.IsAtivo || !usuarioBuscado.IsUsuario)
            {
                _loggingProvider.Erro(new LogDto("UsuarioApplicationService.Login - Usuário não encontrado, inativado ou não é permitido login."));
                return default;
            }

            var test = BCrypt.Net.BCrypt.HashPassword("123");

            if (!PasswordHasher.VerificaPassword(password, usuarioBuscado.Password))
            {
                _loggingProvider.Erro(new LogDto("UsuarioApplicationService.Login - Senha incorreta."));
                return default;
            }

            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.Login - Usuário logado com sucesso."));

            return GenerateJwtToken(username);
        }

        public async Task<UsuarioResponse?> GetByUsername(string? username)
        {
            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.GetByUsername - Iniciando busca do usuário por username."));

            using IDbConnection? conexao = _mySqlConnection.AbrirConexao();

            if (conexao is null)
                return default;

            var usuarioBuscado = await _usuarioService.GetByUsername(conexao, username);

            if (usuarioBuscado is null)
            {
                _loggingProvider.Erro(new LogDto("UsuarioApplicationService.Login - Usuário não encontrado."));
                return default;
            }

            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.Login - Usuário encontrado com sucesso."));

            return _mapper.Map<UsuarioResponse>(usuarioBuscado);
        }

        public async Task<int> Delete(long id)
        {
            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.Delete - Iniciando deleção do usuário."));

            using IDbConnection? conexao = _mySqlConnection.AbrirConexao();

            if (conexao is null)
                return default;

            if (await _usuarioService.Delete(conexao, id) <= 0)
            {
                _loggingProvider.Erro(new LogDto("UsuarioApplicationService.Delete - Usuário não deletado."));
                return default;
            }

            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.Delete - Usuário deletado com sucesso."));

            return USUARIO_DELETADO;
        }

        public async Task<UsuarioResponse?> Update(UsuarioRequestUpdate usuario)
        {
            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.Update - Iniciando update do usuário."));

            using IDbConnection? conexao = _mySqlConnection.AbrirConexao();

            if (conexao is null)
                return default;

            var usuarioBuscado = await _usuarioService.GetByUsername(conexao, usuario.Username);

            if (usuarioBuscado is null)
            {
                _loggingProvider.Erro(new LogDto("UsuarioApplicationService.Login - Usuário não encontrado."));
                return default;
            }

            if (!PasswordHasher.VerificaPassword(usuario.Password, usuarioBuscado.Password))
            {
                _loggingProvider.Erro(new LogDto("UsuarioApplicationService.Login - Senha incorreta."));
                return default;
            }

            usuario.Password = PasswordHasher.HashPassword(usuario.NewPassword);
            var usuarioEntity = _mapper.Map<UsuarioEntity>(usuario);
            usuarioEntity.Id = usuarioBuscado.Id;

            if(string.IsNullOrEmpty(usuario.NewPassword))
                usuarioEntity.Password = usuario.Password;

            var usuarioAtualizado = await _usuarioService.Update(conexao, usuarioEntity);

            if (usuarioAtualizado is null)
            {
                _loggingProvider.Erro(new LogDto("UsuarioApplicationService.Update - Usuário não atualizado."));
                return default;
            }

            _loggingProvider.Informacao(new LogDto("UsuarioApplicationService.Update - Usuário atualizado com sucesso."));
            return _mapper.Map<UsuarioResponse>(usuarioAtualizado);
        }

        #region Métodos Privados
        private string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET_JWT") ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "host-admin-gestao-api",
                audience: "admin-gestao-api",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
