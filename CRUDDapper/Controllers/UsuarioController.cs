using CRUDDapper.DTO;
using CRUDDapper.Models;
using CRUDDapper.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Data.SqlClient;

namespace CRUDDapper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioInterface _usuarioInterface;
        public UsuarioController(IUsuarioInterface usuarioInterface)
        {
            _usuarioInterface = usuarioInterface;
        }

        [HttpGet]
        public async Task<IActionResult> BuscarUsuarios()
        {
            var usuarios = await _usuarioInterface.BuscarUsuarios();

            if(usuarios.Status = false)
            {
                return NotFound(usuarios);
            }
            return Ok(usuarios);
        }

        [HttpGet ("{usuarioId}")]
        public async Task<IActionResult> BuscarUsuarioPorId(int usuarioId)
        {
            var usuarios = await _usuarioInterface.BuscarUsuarioPorId(usuarioId);

            if (usuarios.Status = false)
            {
                return NotFound(usuarios);
            }
            return Ok(usuarios);

        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(UsuarioCriarDTO usuarioCriarDTO)
        {
            var usuarios = await _usuarioInterface.CriarUsuario(usuarioCriarDTO);

            if (usuarios.Status = false)
            {
                return BadRequest(usuarios);
            }
            return Ok(usuarios);

        }

        [HttpPut]
        public async Task<IActionResult> EditarUsuario(UsuarioEditarDTO usuarioEditarDTO)
        {
            var usuarios = await _usuarioInterface.EditarUsuario(usuarioEditarDTO);

            if (usuarios.Status = false)
            {
                return BadRequest(usuarios);
            }
            return Ok(usuarios);

        }

        [HttpDelete]
        public async Task<IActionResult> RemoverUsuario(int usuarioId)
        {
            var usuarios = await _usuarioInterface.RemoverUsuario(usuarioId);

            if (usuarios.Status = false)
            {
                return BadRequest(usuarios);
            }
            return Ok(usuarios);

        }



    }
}
