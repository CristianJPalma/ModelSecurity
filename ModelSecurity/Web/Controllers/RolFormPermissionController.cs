using Business;
using Entity.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RolFormPermissionController : ControllerBase
    {
        private readonly RolFormPermissionBusiness _business;
        private readonly ILogger<RolFormPermissionController> _logger;

        public RolFormPermissionController(RolFormPermissionBusiness business, ILogger<RolFormPermissionController> logger)
        {
            _business = business;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var items = await _business.GetAllAsync();
                return Ok(items);
            }
            catch (ExternalServiceException ex)
            {
                _logger.LogError(ex, "Error al obtener RolFormPermissions");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _business.GetByIdAsync(id);
                return Ok(item);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolFormPermissionDto dto)
        {
            try
            {
                var created = await _business.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolFormPermissionDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "El ID no coincide." });

            try
            {
                await _business.UpdateAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] RolFormPermissionDto dto)
        {
            if (id != dto.Id)
                return BadRequest(new { message = "El ID no coincide." });

            try
            {
                await _business.PatchAsync(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al aplicar patch");
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _business.DeleteAsync(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ExternalServiceException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
