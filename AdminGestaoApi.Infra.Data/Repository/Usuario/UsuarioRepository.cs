using Dapper;
using System.Data;
using AdminGestaoApi.Domain.Entity.Usuario;
using AdminGestaoApi.Domain.Services.Interfaces.Usuario;
using AdminGestaoApi.Infra.Data.Repository.Interfaces;

namespace AdminGestaoApi.Infra.Data.Repository.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMySqlRepository _mySqlRepository;
        private const int USUARIO_DELETADO = 1;
        public UsuarioRepository(IMySqlRepository bancoMySqlRepository)
        {
            _mySqlRepository = bancoMySqlRepository;
        }

        public async Task<UsuarioEntity?> Insert(IDbConnection conexao, UsuarioEntity usuario)
        {
            var query = $@"INSERT INTO {_mySqlRepository.ObterOwnerMySql()}.usuario (nome,username,email,password_hash) VALUES(@Nome,@Username,@Email,@Password);
                           SELECT LAST_INSERT_ID()";

            usuario.Id = await _mySqlRepository.InserirRegistro<int>(conexao, query, usuario);

            if (usuario.Id > 0)
                return usuario;

            return default;
        }

        public async Task<UsuarioEntity?> GetByUsername(IDbConnection conexao, string? username)
        {
            if (string.IsNullOrEmpty(username))
                return default;

            var dictionary = new Dictionary<string, object>
            {
                {"@username", username}
            };

            var parameters = new DynamicParameters(dictionary);

            var query = $@"SELECT id,nome,username,password,email FROM {_mySqlRepository.ObterOwnerMySql()}.usuario
                        WHERE username = @username;";

            return await _mySqlRepository.ObterRegistro<UsuarioEntity>(conexao, query, parameters);
        }

        public async Task<int> Delete(IDbConnection conexao, long id)
        {
            var dictionary = new Dictionary<string, object>
            {
                {"@id", id}
            };

            var parameters = new DynamicParameters(dictionary);

            var query = $@"DELETE FROM {_mySqlRepository.ObterOwnerMySql()}.usuario WHERE id = @id;";

            if (await _mySqlRepository.DeletarRegistro(conexao, query, parameters) > 0)
                return USUARIO_DELETADO;

            return default;
        }

        public async Task<UsuarioEntity?> Update(IDbConnection conexao, UsuarioEntity usuario)
        {
            var query = $@"UPDATE {_mySqlRepository.ObterOwnerMySql()}.usuario SET nome = @Nome, username = @Username, password = @Password, email = @Email WHERE id = @Id;";

            if (await _mySqlRepository.UpdateRegistro(conexao, query, usuario) > 0)
                return usuario;

            return default;
        }
    }
}
