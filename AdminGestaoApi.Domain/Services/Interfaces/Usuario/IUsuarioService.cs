using System.Data;
using AdminGestaoApi.Domain.Entity.Usuario;

namespace AdminGestaoApi.Domain.Services.Interfaces.Usuario
{
    public interface IUsuarioService
    {
        Task<int> Delete(IDbConnection conexao, long id);
        Task<UsuarioEntity?> Update(IDbConnection conexao, UsuarioEntity usuario);
        Task<UsuarioEntity?> Insert(IDbConnection conexao, UsuarioEntity usuario);
        Task<UsuarioEntity?> GetByUsername(IDbConnection conexao, string? username);
    }
}
