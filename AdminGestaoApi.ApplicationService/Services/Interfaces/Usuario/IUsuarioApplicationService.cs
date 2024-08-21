using AdminGestaoApi.ApplicationService.Requests.Usuario;
using AdminGestaoApi.ApplicationService.Responses.Usuario;

namespace AdminGestaoApi.ApplicationService.Services.Interfaces.Usuario
{
    public interface IUsuarioApplicationService
    {
        Task<int> Delete(long id);
        Task<UsuarioResponse?> GetByUsername(string? username);
        Task<string?> Login(string? username, string? password);
        Task<UsuarioResponse?> Update(UsuarioRequestUpdate usuario);
        Task<UsuarioResponse?> Insert(UsuarioRequestInsercao usuario);
    }
}
