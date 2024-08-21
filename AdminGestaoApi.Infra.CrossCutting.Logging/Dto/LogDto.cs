namespace AdminGestaoApi.Infra.CrossCutting.Logging.Dto
{
    public class LogDto
    {
        public LogDto(string log, List<string>? query = default, string? processo = default)
        {
            Log = log;
            Data = DateTime.Now;
            Query = query;
            Processo = processo;
        }

        public string? Log { get; }
        public DateTime Data { get; }
        public List<string>? Query { get; }
        public string? Processo { get; }
    }
}
