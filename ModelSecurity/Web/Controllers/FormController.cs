﻿using Business;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Exceptions;

namespace Web.Controllers
{
    /// <summary>
    /// Controlador para la gestión de formularios en el sistema
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FormController : ControllerBase
    {
        private readonly FormBusiness _FormBusiness;
        private readonly ILogger<FormController> _logger;

        public FormController(FormBusiness FormBusiness, ILogger<FormController> logger)
        {
            _FormBusiness = FormBusiness;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<FormDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllForms()
        {
            try
            {
                var Forms = await _FormBusiness.GetAllFormsAsync();
                return Ok(Forms);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener formularios");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(FormDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetFormById(int id)
        {
            try
            {
                var Form = await _FormBusiness.GetFormByIdAsync(id);
                return Ok(Form);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida para el formulario con ID: {FormId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "formulario no encontrado con ID: {FormId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener formulario con ID: {FormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(FormDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreateForm([FromBody] FormDto FormDto)
        {
            try
            {
                var createdForm = await _FormBusiness.CreateFormAsync(FormDto);
                return CreatedAtAction(nameof(GetFormById), new { id = createdForm.Id }, createdForm);
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al crear formulario");
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al crear formulario");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Actualiza un formulario existente
        /// </summary>
        /// <param name="id">ID del formulario</param>
        /// <param name="FormDto">Datos actualizados del formulario</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateForm(int id, [FromBody] FormDto FormDto)
        {
            if (id != FormDto.Id)
                return BadRequest(new { message = "El ID del formulario no coincide con el del objeto." });

            try
            {
                await _FormBusiness.UpdateFormAsync(FormDto);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, "Validación fallida al actualizar formulario con ID: {FormId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "formulario no encontrado con ID: {FormId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al actualizar formulario con ID: {FormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }


        /// <summary>
        /// Actualiza parcialmente un formulario existente
        /// </summary>
        /// <param name="id">ID del formulario</param>
        /// <param name="FormDto">Datos actualizados del formulario (parciales)</param>
        /// <returns>Resultado de la operación</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PatchForm(int id, [FromBody] FormDto FormDto)
        {
            if (id != FormDto.Id)
                return BadRequest(new { message = "El ID del formulario no coincide con el del objeto." });

            try
            {
                // Llamamos al método de negocio para actualizar parcialmente el formulario
                await _FormBusiness.PatchFormAsync(FormDto);
                return NoContent(); // 204: No Content, éxito pero sin contenido a devolver
            }
            catch (ValidationException ex)
            {
                // Si hay algún error en la validación
                _logger.LogWarning(ex, "Validación fallida al actualizar parcialmente el formulario con ID: {FormId}", id);
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                // Si no se encuentra el formulario
                _logger.LogInformation(ex, "Formulario no encontrado con ID: {FormId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                // Si ocurre algún error al interactuar con el servicio de base de datos o externo
                _logger.LogError(ex, "Error al actualizar parcialmente el formulario con ID: {FormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }



        ///<summary>
        /// <summary>
        /// Desactiva un formulario (eliminación lógica)
        /// </summary>
        /// <param name="id">ID del formulario a desactivar</param>
        /// <returns>NoContent si fue exitoso</returns>
        [HttpDelete("{id}/disable")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DisableForm(int id)
        {
            try
            {
                await _FormBusiness.DisableFormAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "Formulario no encontrado para desactivación con ID: {FormId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al desactivar formulario con ID: {FormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// <summary>
        /// Elimina un formulario por su ID
        /// </summary>
        /// <param name="id">ID del formulario a eliminar</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeleteForm(int id)
        {
            try
            {
                await _FormBusiness.DeleteFormAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogInformation(ex, "formulario no encontrado para eliminación con ID: {FormId}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al eliminar formulario con ID: {FormId}", id);
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}