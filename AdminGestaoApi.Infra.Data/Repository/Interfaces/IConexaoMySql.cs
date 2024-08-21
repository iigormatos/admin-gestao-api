using System.Data;

namespace AdminGestaoApi.Infra.Data.Repository.Interfaces
{
    public interface IConexaoMySql
    {
        IDbConnection? AbrirConexao();
    }
}
