using AdminGestaoApi.Domain.Enum;

namespace AdminGestaoApi.Domain.Entity.Usuario
{
    public class UsuarioEntity
    {
        public long Id { get; set; }

        public string? Nome { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public RoleEnum Role { get; set;}
    }
}
