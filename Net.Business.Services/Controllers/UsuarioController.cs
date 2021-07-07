﻿using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UsuarioController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        public UsuarioController(IRepositoryWrapper repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Obtener lista de calidad
        /// </summary>
        /// <param name="value">Este es el cuerpo para enviar los parametros</param>
        /// <returns>Lista del maestro de Calidad registrado</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>   
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAll([FromQuery] DtoUsuarioFind value)
        {

            var objectGetAll = await _repository.Usuario.GetAll(value.UsuarioFind());

            if (objectGetAll == null)
            {
                return NotFound();
            }

            return Ok(objectGetAll);
        }

        /// <summary>
        /// Obtener un registro individual segun el ID
        /// </summary>
        /// <param name="id">Id de Usuarii</param>
        /// <returns>Devuelve un solo registro</returns>
        /// <response code="200">Devuelve el listado completo </response>
        /// <response code="404">Si no existen datos</response>  
        [HttpGet("{id}", Name = "GetbyIdUsuario")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetbyIdUsuario(int id)
        {
            var objectGetById = await _repository.Usuario.GetById(new DtoUsuarioFind { IdUsuario = id }.UsuarioFind());

            if (objectGetById == null)
            {
                return NotFound();
            }

            return Ok(objectGetById);
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
        public async Task<IActionResult> UpdatePassword([FromBody] DtoUsuarioUpdatePassword value)
        {
            if (value == null)
            {
                return BadRequest(ModelState);
            }

            await _repository.Usuario.UpdatePassword(value.RetornaUsuario());

            return NoContent();
        }
    }
}
