// using Data;
// using Entity.DTOs;
// using Entity.Model;
// using Microsoft.Extensions.Logging;
// using Utilities.Exceptions;

// namespace Business
// {
//     /// <summary>
//     /// Clase de negocio encargada de la lógica relacionada con los RolUser del sistema.
//     /// </summary>
//     public class RolUserBusiness
//     {
//         private readonly RolUserData _rolUserData;
//         private readonly ILogger<RolUserBusiness> _logger;

//         public RolUserBusiness(RolUserData rolUserData, ILogger<RolUserBusiness> logger)
//         {
//             _rolUserData = rolUserData;
//             _logger = logger;
//         }

//         public async Task<IEnumerable<RolUserDto>> GetAllRolUsersAsync()
//         {
//             try
//             {
//                 var rolUsers = await _rolUserData.GetAllAsync();
//                 return rolUsers.Select(MapToDto);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener todos los rolUsers");
//                 throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de rolUsers", ex);
//             }
//         }

//         public async Task<RolUserDto> GetRolUserByIdAsync(int id)
//         {
//             if (id <= 0)
//             {
//                 _logger.LogWarning("Se intentó obtener un rolUser con ID inválido: {RolUserId}", id);
//                 throw new ValidationException("id", "El ID del rolUser debe ser mayor que cero");
//             }

//             try
//             {
//                 var rolUser = await _rolUserData.GetByIdAsync(id);
//                 if (rolUser == null)
//                 {
//                     _logger.LogInformation("No se encontró ningún rolUser con ID: {RolUserId}", id);
//                     throw new EntityNotFoundException("RolUser", id);
//                 }

//                 return MapToDto(rolUser);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener el rolUser con ID: {RolUserId}", id);
//                 throw new ExternalServiceException("Base de datos", $"Error al recuperar el rolUser con ID {id}", ex);
//             }
//         }

//         public async Task<RolUserDto> CreateRolUserAsync(RolUserDto rolUserDto)
//         {
//             try
//             {
//                 ValidateRolUser(rolUserDto);
//                 var rolUser = new RolUser
//                 {
//                     UserId = rolUserDto.UserId,
//                     RolId = rolUserDto.RolId
//                 };

//                 var rolUserCreado = await _rolUserData.CreateAsync(rolUser);
//                 return MapToDto(rolUserCreado);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al crear nuevo rolUser");
//                 throw new ExternalServiceException("Base de datos", "Error al crear el rolUser", ex);
//             }
//         }

//         public async Task UpdateRolUserAsync(RolUserDto rolUserDto)
//         {
//             if (rolUserDto == null || rolUserDto.Id <= 0)
//                 throw new ValidationException("Id", "El rolUser a actualizar debe tener un ID válido");

//             ValidateRolUser(rolUserDto);

//             try
//             {
//                 var existing = await _rolUserData.GetByIdAsync(rolUserDto.Id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("RolUser", rolUserDto.Id);

//                 existing.UserId = rolUserDto.UserId;
//                 existing.RolId = rolUserDto.RolId;

//                 var result = await _rolUserData.UpdateAsync(existing);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "Error al actualizar el rolUser");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar rolUser con ID: {RolUserId}", rolUserDto.Id);
//                 throw;
//             }
//         }

//         public async Task PatchRolUserAsync(RolUserDto rolUserDto)
//         {
//             if (rolUserDto == null || rolUserDto.Id <= 0)
//                 throw new ValidationException("Id", "El rolUser a actualizar debe tener un ID válido");

//             try
//             {
//                 var existing = await _rolUserData.GetByIdAsync(rolUserDto.Id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("RolUser", rolUserDto.Id);

//                 if (rolUserDto.UserId != 0)
//                     existing.UserId = rolUserDto.UserId;

//                 if (rolUserDto.RolId != 0)
//                     existing.RolId = rolUserDto.RolId;

//                 var result = await _rolUserData.UpdateAsync(existing);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "Error al actualizar el rolUser");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar parcialmente el rolUser con ID: {RolUserId}", rolUserDto.Id);
//                 throw;
//             }
//         }

//         public async Task DeleteRolUserAsync(int id)
//         {
//             if (id <= 0)
//                 throw new ValidationException("id", "El ID del RolUser debe ser mayor que cero");

//             try
//             {
//                 var existing = await _rolUserData.GetByIdAsync(id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("RolUser", id);

//                 var result = await _rolUserData.DeleteAsync(id);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "No se pudo eliminar el rolUser");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al eliminar rolUser con ID: {RolUserId}", id);
//                 throw;
//             }
//         }

//         private void ValidateRolUser(RolUserDto rolUserDto)
//         {
//             if (rolUserDto == null)
//             {
//                 throw new ValidationException("rolUser", "El objeto rolUser no puede ser nulo");
//             }
//         }

//         private RolUserDto MapToDto(RolUser rolUser)
//         {
//             return new RolUserDto
//             {
//                 Id = rolUser.Id,
//                 UserId = rolUser.UserId,
//                 RolId = rolUser.RolId
//             };
//         }
//     }
// }
