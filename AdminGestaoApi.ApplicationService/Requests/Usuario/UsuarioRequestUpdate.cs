namespace AdminGestaoApi.ApplicationService.Requests.Usuario
{
    public class UsuarioRequestUpdate : UsuarioRequestBase
    {
        public string? Nome { get; set; }

        public string? Email { get; set; }

        public string? NewPassword { get; set; }
    }
}
