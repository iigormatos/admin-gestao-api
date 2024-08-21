using System.Data;

namespace AdminGestaoApi.ApplicationService.Services.Interfaces.Data
{
    public interface IMySqlConnection
    {
        IDbConnection? AbrirConexao();
    }
}
