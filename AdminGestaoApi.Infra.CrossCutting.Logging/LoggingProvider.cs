using System.Text.Json;
using Microsoft.Extensions.Logging;
using AdminGestaoApi.Infra.CrossCutting.Logging.Dto;
using AdminGestaoApi.Infra.CrossCutting.Logging.Interfaces;

namespace AdminGestaoApi.Infra.CrossCutting.Logging
{
    public class LoggingProvider : ILoggingProvider
    {
        private readonly ILogger<LoggingProvider> _logger;
        private readonly bool _logInformacao;

        public LoggingProvider(ILogger<LoggingProvider> logger)
        {
            _logger = logger;
            _logInformacao = bool.TryParse(Environment.GetEnvironmentVariable("LOG_INFORMACAO"), out bool logInformacao)
                && logInformacao;
        }

        public void Informacao(LogDto log, bool forcar = false)
        {
            if (log != null && (_logInformacao || (!_logInformacao && forcar)))
            {
                var json = JsonSerializer.Serialize(log, ObterOpcoesSerializacao());
                _logger.LogInformation(json);
            }
        }

        public void Aviso(LogDto log)
        {
            if (log != null)
            {
                var json = JsonSerializer.Serialize(log, ObterOpcoesSerializacao());
                _logger.LogWarning(json);
            }
        }

        public void Erro(LogDto log)
        {
            if (log != null)
            {
                var json = JsonSerializer.Serialize(log, ObterOpcoesSerializacao());
                _logger.LogError(json);
            }
        }

        private static JsonSerializerOptions ObterOpcoesSerializacao()
        {
            return new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }
    }
}
