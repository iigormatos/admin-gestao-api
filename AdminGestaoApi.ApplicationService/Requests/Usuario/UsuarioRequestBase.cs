using AdminGestaoApi.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace AdminGestaoApi.ApplicationService.Requests.Usuario
{
    public class UsuarioRequestBase
    {
        [Required(ErrorMessage = "O campo Username é obrigatório.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "O campo Password é obrigatório.")]
        public string? Password { get; set; }
    }
}
