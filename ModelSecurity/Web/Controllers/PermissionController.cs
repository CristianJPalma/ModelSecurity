// using Business;
// using Entity.DTOs;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Utilities.Exceptions;

// namespace Web.Controllers
// {
//     /// <summary>
//     /// Controlador para la gestión de permisos en el sistema
//     /// </summary>
//     [Route("api/[controller]")]
//     [ApiController]
//     [Produces("application/json")]
//     public class PermissionController : ControllerBase
//     {
//         private readonly PermissionBusiness _permissionBusiness;
//         private readonly ILogger<PermissionController> _logger;

//         public PermissionController(PermissionBusiness permissionBusiness, ILogger<PermissionController> logger)
//         {
//             _permissionBusiness = permissionBusiness;
//             _logger = logger;
//         }

//         [HttpGet]
//         [ProducesResponseType(typeof(IEnumerable<PermissionDto>), 200)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> GetAllPermissions()
//         {
//             try
//             {
//                 var permissions = await _permissionBusiness.GetAllPermissionsAsync();
//                 return Ok(permissions);
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al obtener permisos");
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpGet("{id}")]
//         [ProducesResponseType(typeof(PermissionDto), 200)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> GetPermissionById(int id)
//         {
//             try
//             {
//                 var permission = await _permissionBusiness.GetPermissionByIdAsync(id);
//                 return Ok(permission);
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida para el permiso con ID: {PermissionId}", id);
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "Permiso no encontrado con ID: {PermissionId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al obtener permiso con ID: {PermissionId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpPost]
//         [ProducesResponseType(typeof(PermissionDto), 201)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> CreatePermission([FromBody] PermissionDto permissionDto)
//         {
//             try
//             {
//                 var createdPermission = await _permissionBusiness.CreatePermissionAsync(permissionDto);
//                 return CreatedAtAction(nameof(GetPermissionById), new { id = createdPermission.Id }, createdPermission);
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida al crear permiso");
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al crear permiso");
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpPut("{id}")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> UpdatePermission(int id, [FromBody] PermissionDto permissionDto)
//         {
//             if (id != permissionDto.Id)
//                 return BadRequest(new { message = "El ID del permiso no coincide con el del objeto." });

//             try
//             {
//                 await _permissionBusiness.UpdatePermissionAsync(permissionDto);
//                 return NoContent();
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida al actualizar permiso con ID: {PermissionId}", id);
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "Permiso no encontrado con ID: {PermissionId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar permiso con ID: {PermissionId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpPatch("{id}")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> PatchPermission(int id, [FromBody] PermissionDto permissionDto)
//         {
//             if (id != permissionDto.Id)
//                 return BadRequest(new { message = "El ID del permiso no coincide con el del objeto." });

//             try
//             {
//                 await _permissionBusiness.PatchPermissionAsync(permissionDto);
//                 return NoContent();
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida al actualizar parcialmente el permiso con ID: {PermissionId}", id);
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "Permiso no encontrado con ID: {PermissionId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar parcialmente el permiso con ID: {PermissionId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpDelete("{id}/disable")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> DisablePermission(int id)
//         {
//             try
//             {
//                 await _permissionBusiness.DisablePermissionAsync(id);
//                 return NoContent();
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "Permiso no encontrado para desactivación con ID: {PermissionId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al desactivar permiso con ID: {PermissionId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpDelete("{id}")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> DeletePermission(int id)
//         {
//             try
//             {
//                 await _permissionBusiness.DeletePermissionAsync(id);
//                 return NoContent();
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "Permiso no encontrado para eliminación con ID: {PermissionId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al eliminar permiso con ID: {PermissionId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }
//     }
// }
