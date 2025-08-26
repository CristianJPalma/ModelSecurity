// using Business;
// using Entity.DTOs;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Utilities.Exceptions;

// namespace Web.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     [Produces("application/json")]
//     public class RolUserController : ControllerBase
//     {
//         private readonly RolUserBusiness _rolUserBusiness;
//         private readonly ILogger<RolUserController> _logger;

//         public RolUserController(RolUserBusiness rolUserBusiness, ILogger<RolUserController> logger)
//         {
//             _rolUserBusiness = rolUserBusiness;
//             _logger = logger;
//         }

//         [HttpGet]
//         [ProducesResponseType(typeof(IEnumerable<RolUserDto>), 200)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> GetAllRolUsers()
//         {
//             try
//             {
//                 var rolUsers = await _rolUserBusiness.GetAllRolUsersAsync();
//                 return Ok(rolUsers);
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al obtener usuarios con roles");
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpGet("{id}")]
//         [ProducesResponseType(typeof(RolUserDto), 200)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> GetRolUserById(int id)
//         {
//             try
//             {
//                 var rolUser = await _rolUserBusiness.GetRolUserByIdAsync(id);
//                 return Ok(rolUser);
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida para el rolUser con ID: {RolUserId}", id);
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "RolUser no encontrado con ID: {RolUserId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al obtener rolUser con ID: {RolUserId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpPost]
//         [ProducesResponseType(typeof(RolUserDto), 201)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> CreateRolUser([FromBody] RolUserDto rolUserDto)
//         {
//             try
//             {
//                 var createdRolUser = await _rolUserBusiness.CreateRolUserAsync(rolUserDto);
//                 return CreatedAtAction(nameof(GetRolUserById), new { id = createdRolUser.Id }, createdRolUser);
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida al crear rolUser");
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al crear rolUser");
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpPut("{id}")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> UpdateRolUser(int id, [FromBody] RolUserDto rolUserDto)
//         {
//             if (id != rolUserDto.Id)
//                 return BadRequest(new { message = "El ID del rolUser no coincide con el del objeto." });

//             try
//             {
//                 await _rolUserBusiness.UpdateRolUserAsync(rolUserDto);
//                 return NoContent();
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida al actualizar rolUser con ID: {RolUserId}", id);
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "RolUser no encontrado con ID: {RolUserId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar rolUser con ID: {RolUserId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpPatch("{id}")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> PatchRolUser(int id, [FromBody] RolUserDto rolUserDto)
//         {
//             if (id != rolUserDto.Id)
//                 return BadRequest(new { message = "El ID del rolUserDto no coincide con el del objeto." });

//             try
//             {
//                 await _rolUserBusiness.PatchRolUserAsync(rolUserDto);
//                 return NoContent();
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida al actualizar parcialmente el rolUserDto con ID: {RolUserId}", id);
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "RolUser no encontrado con ID: {RolUserId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar parcialmente el rolUser con ID: {RolUserId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpDelete("{id}")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> DeleteRolUser(int id)
//         {
//             try
//             {
//                 await _rolUserBusiness.DeleteRolUserAsync(id);
//                 return NoContent();
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "RolUser no encontrado para eliminación con ID: {RolUserId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al eliminar rolUser con ID: {RolUserId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }
//     }
// }
