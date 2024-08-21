using System.Data;
using AdminGestaoApi.Domain.Entity.Usuario;
using AdminGestaoApi.Infra.CrossCutting.Logging.Dto;
using AdminGestaoApi.Domain.Services.Interfaces.Usuario;
using AdminGestaoApi.Infra.CrossCutting.Logging.Interfaces;

namespace AdminGestaoApi.Domain.Services.Usuario
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ILoggingProvider _loggingProvider;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository, ILoggingProvider loggingProvider)
        {
            _usuarioRepository = usuarioRepository;
            _loggingProvider = loggingProvider;
        }
        public async Task<UsuarioEntity?> Insert(IDbConnection conexao, UsuarioEntity usuario)
        {
            _loggingProvider.Informacao(new LogDto("UsuarioService.Insert - Iniciando inserção de usuário."));
            return await _usuarioRepository.Insert(conexao, usuario);
        }

        public async Task<UsuarioEntity?> GetByUsername(IDbConnection conexao, string? username)
        {
            _loggingProvider.Informacao(new LogDto("UsuarioService.GetByUsername - Iniciando busca de usuário por username."));
            return await _usuarioRepository.GetByUsername(conexao, username);
        }

        public async Task<int> Delete(IDbConnection conexao, long id)
        {
            _loggingProvider.Informacao(new LogDto("UsuarioService.Delete - Iniciando deleção de usuário por id."));
            return await _usuarioRepository.Delete(conexao, id);
        }

        public async Task<UsuarioEntity?> Update(IDbConnection conexao, UsuarioEntity usuario)
        {
            _loggingProvider.Informacao(new LogDto("UsuarioService.Update - Iniciando update do usuário."));
            return await _usuarioRepository.Update(conexao, usuario);
        }
    }
}
