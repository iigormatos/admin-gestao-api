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

        public bool IsAtivo { get; set; }
        
        public bool IsUsuario { get; set; }
        
        public string? Celular { get; set; }
        
        public string? RedeSocial { get; set; }

        public string? Endereco { get; set; }

        public string? Cidade { get; set; }

        public string? Estado { get; set; }
    }
}
