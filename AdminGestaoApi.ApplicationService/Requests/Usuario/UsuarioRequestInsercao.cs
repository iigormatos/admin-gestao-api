using AdminGestaoApi.Domain.Enum;

namespace AdminGestaoApi.ApplicationService.Requests.Usuario
{
    public class UsuarioRequestInsercao : UsuarioRequestBase
    {
        public string? Nome { get; set; }

        public string? Email { get; set; }

        public RoleEnum Role { get; set; }

        public string? Celular { get; set; }

        public string? RedeSocial { get; set; }

        public bool IsAtivo { get; set; }

        public bool IsUsuario { get; set; }

        public string? Endereco { get; set; }

        public string? Cidade { get; set; }

        public string? Estado { get; set; }
    }
}
