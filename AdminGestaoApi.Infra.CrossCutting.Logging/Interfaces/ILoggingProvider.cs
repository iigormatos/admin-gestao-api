using AdminGestaoApi.Infra.CrossCutting.Logging.Dto;

namespace AdminGestaoApi.Infra.CrossCutting.Logging.Interfaces
{
    public interface ILoggingProvider
    {
        void Aviso(LogDto log);

        void Erro(LogDto log);

        void Informacao(LogDto log, bool forcar = false);
    }
}
