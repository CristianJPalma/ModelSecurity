using Business.Interfaces;
using Entity.DTOs;
using Entity.Model;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Implements
{
    [ApiController]
    public abstract class BaseController<T, D> : ControllerBase where T : BaseModel where D : BaseDto
    {
        protected readonly IBaseBusiness<T, D> _business;
        protected readonly ILogger<BaseController<T, D>> _logger;

        protected BaseController(IBaseBusiness<T, D> business, ILogger<BaseController<T, D>> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            try
            {
                var entities = await _business.GetAllAsync();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los registros.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int id)
        {
            try
            {
                var entity = await _business.GetByIdAsync(id);
                if (entity == null)
                    return NotFound($"Registro con ID {id} no encontrado");

                return Ok(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener registro con ID {id}.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] D dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdEntity = await _business.CreateAsync(dto);

                // Ya accedes directo al Id desde el dto creado
                return CreatedAtAction(nameof(GetById), new { id = createdEntity.Id }, createdEntity);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error de validación al crear el registro.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el registro.");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        [HttpPut]
        public virtual async Task<IActionResult> Update([FromBody] D dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedEntity = await _business.UpdateAsync(dto);
                return Ok(updatedEntity);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error de validación al actualizar el registro.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el registro.");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _business.DeleteAsync(id);
                if (!result)
                    return NotFound($"Registro con ID {id} no encontrado");

                return Ok(new { Success = true });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar registro con ID {id}.");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}