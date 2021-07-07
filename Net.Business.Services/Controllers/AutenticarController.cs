using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.Business.DTO;
using Net.Data;
using System.Threading.Tasks;

namespace Net.Business.Services.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ApiExplorerSettings(GroupName = "ApiSeguridad")]
    public class AutenticarController : ControllerBase
    {
        private readonly IRepositoryWrapper repository;

        public AutenticarController(IRepositoryWrapper repository)
        {
            this.repository = repository;
        }

        [HttpPost]
        public IActionResult Autenticar([FromBody] DtoUsuarioAutenticarRequest request)
        {
            var response = this.repository.Usuario.Autenticar(request.UsuarioAutenticar());
            if (response.Result.ResultadoCodigo < 0)
            {
                return BadRequest(response.Result);
            }

            return Ok(response.Result.data);
        }

        [HttpPost]
        public IActionResult ObtienePermisosPorUsuario([FromBody] DtoUsuarioDatosRequest request)
        {
            var response = this.repository.Usuario.ObtienePermisosPorUsuario(request.UsuarioDatos());
            
            if (response == null)
            {
                return BadRequest("Credenciales incorrectas");
            }

            return Ok(response.Result);
        }

        /// <summary>
        /// Actualizar un registro existente
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <response code="204">Actualizado Satisfactoriamente</response>
        /// <response code="404">Si el objeto enviado es nulo o invalido</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RecuperarPassword([FromBody] DtoUsuarioRecuperarClave value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await this.repository.Usuario.RecuperarPassword(value.RetornaUsuario());

            return NoContent();
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AutenticarCredenciales([FromBody] DtoUsuarioAutenticarRequest request)
        {
            var response = this.repository.Usuario.AutenticarUsuario(request.UsuarioAutenticar());
            if (response.Result.ResultadoCodigo == -1)
            {
                return BadRequest(response.Result);
            }

            return Ok(response.Result.data);
        }
    }
}