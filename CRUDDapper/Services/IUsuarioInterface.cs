using CRUDDapper.DTO;
using CRUDDapper.Models;

namespace CRUDDapper.Services
{
    public interface IUsuarioInterface
    {
        Task<ResponseModel<List<UsuarioListarDTO>>> BuscarUsuarios();

        Task<ResponseModel<UsuarioListarDTO>> BuscarUsuarioPorId(int usuarioId);
        Task<ResponseModel<List<UsuarioListarDTO>>> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO);
        Task<ResponseModel<List<UsuarioListarDTO>>> EditarUsuario(UsuarioEditarDTO usuarioEditarDTO);
        Task<ResponseModel<List<UsuarioListarDTO>>> RemoverUsuario(int usuarioId);

    }
}
