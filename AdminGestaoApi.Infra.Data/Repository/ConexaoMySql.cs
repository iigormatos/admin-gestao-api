using System.Data;
using MySqlConnector;
using AdminGestaoApi.Infra.Data.Exceptions;
using AdminGestaoApi.Infra.Data.Repository.Interfaces;

namespace AdminGestaoApi.Infra.Data.Repository
{
    public class ConexaoMySql : IConexaoMySql
    {
        private readonly string _textoConexao;

        public ConexaoMySql(string textoConexao)
        {
            _textoConexao = textoConexao;
        }

        public ConexaoMySql()
        {
            _textoConexao = MontarTextoConexao();
        }

        public IDbConnection AbrirConexao()
        {
            var conexao = new MySqlConnection(_textoConexao);

            if (conexao.State == ConnectionState.Broken)
            {
                conexao.Close();
                conexao.Open();
            }

            if (conexao.State == ConnectionState.Closed)
            {
                conexao.Open();
            }

            return conexao;
        }

        private string MontarTextoConexao()
        {
            string? url = Environment.GetEnvironmentVariable("MYSQL_URL");
            string? port = Environment.GetEnvironmentVariable("MYSQL_PORT");
            string? usuario = Environment.GetEnvironmentVariable("MYSQL_USER");
            string? senha = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            string? database = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            string? minPool = Environment.GetEnvironmentVariable("MYSQL_MINPOOLSIZE");
            string? maxPool = Environment.GetEnvironmentVariable("MYSQL_MAXPOOLSIZE");

            if (string.IsNullOrEmpty(url))
                throw new MySqlEnvironmentVarNotConfiguredException("MYSQL_URL");

            if (string.IsNullOrEmpty(port))
                throw new MySqlEnvironmentVarNotConfiguredException("MYSQL_PORT");

            if (string.IsNullOrEmpty(usuario))
                throw new MySqlEnvironmentVarNotConfiguredException("MYSQL_USER");

            if (string.IsNullOrEmpty(senha))
                throw new MySqlEnvironmentVarNotConfiguredException("MYSQL_PASSWORD");

            if (string.IsNullOrEmpty(database))
                throw new MySqlEnvironmentVarNotConfiguredException("MYSQL_DATABASE");

            if (string.IsNullOrEmpty(minPool))
                minPool = "5";

            if (string.IsNullOrEmpty(maxPool))
                maxPool = "10";

            return $"server={url};port={port};userid={usuario};password={senha};database={database};MinimumPoolSize={minPool};maximumpoolsize={maxPool};SslMode=none";
        }
    }
}
