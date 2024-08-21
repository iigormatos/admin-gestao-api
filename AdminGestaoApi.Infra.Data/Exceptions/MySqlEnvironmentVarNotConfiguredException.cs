namespace AdminGestaoApi.Infra.Data.Exceptions
{
    public class MySqlEnvironmentVarNotConfiguredException : Exception
    {
        public MySqlEnvironmentVarNotConfiguredException(string var) : base(string.Format("{0} variável de ambiente do MySql não configurada corretamente!", var)) { }
    }
}
