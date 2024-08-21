using System.Data;

namespace AdminGestaoApi.Infra.Data.Repository.Interfaces
{
    public interface IMySqlRepository
    {
        Task<T?> ObterRegistro<T>(IDbConnection conexao, string instrucao, object? parametros = null);
        Task<IEnumerable<T>?> ObterRegistros<T>(IDbConnection conexao, string instrucao, object? parametros = null);
        Task<T?> InserirRegistro<T>(IDbConnection conexao, string instrucao, object? parametros = null);
        Task<int?> UpdateRegistro(IDbConnection conexao, string instrucao, object? parametros = null);
        Task<int?> DeletarRegistro(IDbConnection conexao, string instrucao, object? parametros = null);
        string? ObterOwnerMySql();
    }
}
