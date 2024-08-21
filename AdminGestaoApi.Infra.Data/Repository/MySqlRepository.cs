using Dapper;
using System.Data;
using AdminGestaoApi.Infra.CrossCutting.Logging.Dto;
using AdminGestaoApi.Infra.Data.Repository.Interfaces;
using AdminGestaoApi.Infra.CrossCutting.Logging.Interfaces;
using AdminGestaoApi.ApplicationService.Services.Interfaces.Data;

namespace AdminGestaoApi.Infra.Data.Repository
{
    public class MySqlRepository : IMySqlRepository, IMySqlConnection
    {
        private readonly IConexaoMySql _conexaoMySql;
        private readonly ILoggingProvider _loggingProvider;

        public MySqlRepository(IConexaoMySql conexaoMySql, ILoggingProvider loggingProvider)
        {
            _conexaoMySql = conexaoMySql;
            _loggingProvider = loggingProvider;
        }

        public IDbConnection? AbrirConexao()
        {
            _loggingProvider.Informacao(new LogDto("MySqlRepository.AbrirConexao - Abrindo conexão."));

            var conexao = _conexaoMySql.AbrirConexao();

            if (conexao != null && conexao.State == ConnectionState.Open)
            {
                _loggingProvider.Informacao(new LogDto("MySqlRepository.AbrirConexao - Conexão aberta."));
                return conexao;
            }

            _loggingProvider.Informacao(new LogDto("MySqlRepository.AbrirConexao - Falha na conexão."));

            return default;
        }

        public string? ObterOwnerMySql()
        {
            return Environment.GetEnvironmentVariable("OWNER_MYSQL");
        }

        public async Task<T?> ObterRegistro<T>(IDbConnection conexao, string instrucao, object? parametros = null)
        {
            try
            {
                return await conexao.QueryFirstOrDefaultAsync<T>(instrucao, parametros);
            }
            catch (Exception ex)
            {
                _loggingProvider.Erro(new LogDto($"[ObterRegistro] Erro ao obter registro do banco | {ex.Message.Trim()}", new List<string> { instrucao }, nameof(ObterRegistros)));
                throw new InvalidOperationException(ex.Message.Trim(), ex);
            }
        }

        public async Task<IEnumerable<T>?> ObterRegistros<T>(IDbConnection conexao, string instrucao, object? parametros = null)
        {
            try
            {
                return await conexao.QueryAsync<T>(instrucao, parametros);
            }
            catch (Exception ex)
            {
                _loggingProvider.Erro(new LogDto($"[ObterRegistros] Erro ao obter registros do banco | {ex.Message.Trim()}", new List<string> { instrucao }, nameof(ObterRegistros)));

                throw new InvalidOperationException(ex.Message.Trim(), ex);
            }
        }

        public async Task<T?> InserirRegistro<T>(IDbConnection conexao, string instrucao, object? parametros = null)
        {
            try
            {
                return await conexao.ExecuteScalarAsync<T>(instrucao, parametros);
            }
            catch (Exception ex)
            {
                _loggingProvider.Erro(new LogDto($"[InserirRegistro] Erro ao inserir registro do banco MySql | {ex.Message.Trim()}", new List<string> { instrucao }, nameof(ObterRegistros)));
                throw new InvalidOperationException(ex.Message.Trim(), ex);
            }
        }

        public async Task<int?> UpdateRegistro(IDbConnection conexao, string instrucao, object? parametros = null)
        {
            try
            {
                return await conexao.ExecuteAsync(instrucao, parametros);
            }
            catch (Exception ex)
            {
                _loggingProvider.Erro(new LogDto($"[UpdateRegistro] Erro ao atualizar registros do banco MySql | {ex.Message.Trim()}", new List<string> { instrucao }, nameof(ObterRegistros)));
                throw new InvalidOperationException(ex.Message.Trim(), ex);
            }
        }

        public async Task<int?> DeletarRegistro(IDbConnection conexao, string instrucao, object? parametros = null)
        {
            try
            {
                return await conexao.ExecuteAsync(instrucao, parametros);
            }
            catch (Exception ex)
            {
                _loggingProvider.Erro(new LogDto($"[DeletarRegistro] Erro ao deletar registros do banco MySql | {ex.Message.Trim()}", new List<string> { instrucao }, nameof(ObterRegistros)));
                throw new InvalidOperationException(ex.Message.Trim(), ex);
            }
        }
    }
}
