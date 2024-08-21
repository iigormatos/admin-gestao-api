namespace AdminGestaoApi.ApplicationService.Requests.Usuario
{
    public class UsuarioRequestInsercao : UsuarioRequestBase
    {
        public string? Nome { get; set; }

        public string? Email { get; set; }
    }
}
