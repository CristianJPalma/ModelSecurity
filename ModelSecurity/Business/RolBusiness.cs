// using Data;
// using Entity.DTOs;
// using Entity.Model;
// using Microsoft.Extensions.Logging;
// using Utilities.Exceptions;

// namespace Business
// {
//     /// <summary>
//     /// Clase de negocio encargada de la lógica relacionada con los roles del sistema.
//     /// </summary>
//     public class RolBusiness
//     {
//         private readonly RolData _rolData;
//         private readonly ILogger<RolBusiness> _logger;

//         public RolBusiness(RolData rolData, ILogger<RolBusiness> logger)
//         {
//             _rolData = rolData;
//             _logger = logger;
//         }

//         public async Task<IEnumerable<RolDto>> GetAllRolesAsync()
//         {
//             try
//             {
//                 var roles = await _rolData.GetAllAsync();
//                 return roles.Select(MapToDto);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener todos los roles");
//                 throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de roles", ex);
//             }
//         }

//         public async Task<RolDto> GetRolByIdAsync(int id)
//         {
//             if (id <= 0)
//                 throw new ValidationException("id", "El ID del rol debe ser mayor que cero");

//             try
//             {
//                 var rol = await _rolData.GetByIdAsync(id);
//                 if (rol == null)
//                     throw new EntityNotFoundException("Rol", id);

//                 return MapToDto(rol);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener el rol con ID: {RolId}", id);
//                 throw new ExternalServiceException("Base de datos", $"Error al recuperar el rol con ID {id}", ex);
//             }
//         }

//         public async Task<RolDto> CreateRolAsync(RolDto rolDto)
//         {
//             try
//             {
//                 ValidateRol(rolDto);
//                 var rol = new Rol
//                 {
//                     Name = rolDto.Name,
//                     Description = rolDto.Description,
//                     Active = rolDto.Active,
//                     CreateAt = DateTime.Now
//                 };

//                 var rolCreado = await _rolData.CreateAsync(rol);
//                 return MapToDto(rolCreado);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al crear rol: {RolNombre}", rolDto?.Name ?? "null");
//                 throw new ExternalServiceException("Base de datos", "Error al crear el rol", ex);
//             }
//         }

//         public async Task UpdateRolAsync(RolDto rolDto)
//         {
//             if (rolDto == null || rolDto.Id <= 0)
//                 throw new ValidationException("Id", "El rol a actualizar debe tener un ID válido");

//             ValidateRol(rolDto);

//             try
//             {
//                 var existing = await _rolData.GetByIdAsync(rolDto.Id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("Rol", rolDto.Id);

//                 existing.Name = rolDto.Name;
//                 existing.Description = rolDto.Description;
//                 existing.Active = rolDto.Active;

//                 var result = await _rolData.UpdateAsync(existing);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "Error al actualizar el rol");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar rol con ID: {RolId}", rolDto.Id);
//                 throw;
//             }
//         }

//         /// <summary>
//         /// Actualiza parcialmente un rol mediante un DTO.
//         /// </summary>
//         public async Task PatchRolAsync(RolDto rolDto)
//         {
//             if (rolDto == null || rolDto.Id <= 0)
//                 throw new ValidationException("Id", "El rol a actualizar debe tener un ID válido");

//             try
//             {
//                 var existing = await _rolData.GetByIdAsync(rolDto.Id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("Rol", rolDto.Id);

//                 if (!string.IsNullOrEmpty(rolDto.Name))
//                     existing.Name = rolDto.Name;

//                 if (!string.IsNullOrEmpty(rolDto.Description))
//                     existing.Description = rolDto.Description;

//                 if (rolDto.Active != null)
//                     existing.Active = rolDto.Active;

//                 var result = await _rolData.UpdateAsync(existing);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "Error al actualizar el rol");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar parcialmente el rol con ID: {RolId}", rolDto.Id);
//                 throw;
//             }
//         }

//         /// <summary>
//         /// Realiza una eliminación lógica del rol.
//         /// </summary>
//         public async Task DisableRolAsync(int id)
//         {
//             if (id <= 0)
//                 throw new ValidationException("id", "El ID del rol debe ser mayor que cero");

//             try
//             {
//                 var existing = await _rolData.GetByIdAsync(id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("Rol", id);

//                 var result = await _rolData.DisableAsync(id);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "No se pudo desactivar el rol");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al desactivar rol con ID: {RolId}", id);
//                 throw;
//             }
//         }

//         /// <summary>
//         /// Realiza una eliminación total del rol.
//         /// </summary>
//         public async Task DeleteRolAsync(int id)
//         {
//             if (id <= 0)
//                 throw new ValidationException("id", "El ID del rol debe ser mayor que cero");

//             try
//             {
//                 var existing = await _rolData.GetByIdAsync(id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("Rol", id);

//                 var result = await _rolData.DeleteAsync(id);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "No se pudo eliminar el rol");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al eliminar rol con ID: {RolId}", id);
//                 throw;
//             }
//         }

//         private void ValidateRol(RolDto rolDto)
//         {
//             if (rolDto == null)
//                 throw new ValidationException("rolDto", "El objeto rol no puede ser nulo");

//             if (string.IsNullOrWhiteSpace(rolDto.Description))
//                 throw new ValidationException("Description", "La Description del rol es obligatorio");
//         }

//         private RolDto MapToDto(Rol rol)
//         {
//             return new RolDto
//             {
//                 Id = rol.Id,
//                 Name = rol.Name,
//                 Description = rol.Description,
//                 Active = rol.Active
//             };
//         }
//     }
// }
