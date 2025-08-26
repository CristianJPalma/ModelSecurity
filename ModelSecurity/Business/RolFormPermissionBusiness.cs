// using Data;
// using Entity.DTOs;
// using Entity.Model;
// using Microsoft.Extensions.Logging;
// using Utilities.Exceptions;

// namespace Business
// {
//     public class RolFormPermissionBusiness
//     {
//         private readonly RolFormPermissionData _data;
//         private readonly ILogger<RolFormPermissionBusiness> _logger;

//         public RolFormPermissionBusiness(RolFormPermissionData data, ILogger<RolFormPermissionBusiness> logger)
//         {
//             _data = data;
//             _logger = logger;
//         }

//         public async Task<IEnumerable<RolFormPermissionDto>> GetAllAsync()
//         {
//             try
//             {
//                 var items = await _data.GetAllAsync();
//                 return items.Select(MapToDto);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener RolFormPermissions");
//                 throw new ExternalServiceException("Base de datos", "Error al obtener la lista de RolFormPermissions", ex);
//             }
//         }

//         public async Task<RolFormPermissionDto> GetByIdAsync(int id)
//         {
//             if (id <= 0)
//                 throw new ValidationException("id", "El ID debe ser mayor que cero");

//             try
//             {
//                 var entity = await _data.GetByIdAsync(id);
//                 if (entity == null)
//                     throw new EntityNotFoundException("RolFormPermission", id);

//                 return MapToDto(entity);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, $"Error al obtener RolFormPermission con ID {id}");
//                 throw;
//             }
//         }

//         public async Task<RolFormPermissionDto> CreateAsync(RolFormPermissionDto dto)
//         {
//             ValidateDto(dto);

//             try
//             {
//                 var entity = new RolFormPermission
//                 {
//                     RolId = dto.RolId,
//                     FormId = dto.FormId,
//                     PermissionId = dto.PermissionId
//                 };

//                 var created = await _data.CreateAsync(entity);
//                 return MapToDto(created);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al crear RolFormPermission");
//                 throw;
//             }
//         }

//         public async Task UpdateAsync(RolFormPermissionDto dto)
//         {
//             if (dto.Id <= 0)
//                 throw new ValidationException("Id", "ID inválido");

//             ValidateDto(dto);

//             var existing = await _data.GetByIdAsync(dto.Id);
//             if (existing == null)
//                 throw new EntityNotFoundException("RolFormPermission", dto.Id);

//             existing.RolId = dto.RolId;
//             existing.FormId = dto.FormId;
//             existing.PermissionId = dto.PermissionId;

//             if (!await _data.UpdateAsync(existing))
//                 throw new ExternalServiceException("Base de datos", "No se pudo actualizar");
//         }

//         public async Task PatchAsync(RolFormPermissionDto dto)
//         {
//             if (dto.Id <= 0)
//                 throw new ValidationException("Id", "ID inválido");

//             var existing = await _data.GetByIdAsync(dto.Id);
//             if (existing == null)
//                 throw new EntityNotFoundException("RolFormPermission", dto.Id);

//             if (dto.RolId != 0)
//                 existing.RolId = dto.RolId;

//             if (dto.FormId != 0)
//                 existing.FormId = dto.FormId;

//             if (dto.PermissionId != 0)
//                 existing.PermissionId = dto.PermissionId;

//             if (!await _data.UpdateAsync(existing))
//                 throw new ExternalServiceException("Base de datos", "Error al aplicar cambios parciales");
//         }

//         public async Task DeleteAsync(int id)
//         {
//             var existing = await _data.GetByIdAsync(id);
//             if (existing == null)
//                 throw new EntityNotFoundException("RolFormPermission", id);

//             if (!await _data.DeleteAsync(id))
//                 throw new ExternalServiceException("Base de datos", "Error al eliminar");
//         }

//         private void ValidateDto(RolFormPermissionDto dto)
//         {
//             if (dto == null)
//                 throw new ValidationException("dto", "El objeto no puede ser nulo");
//         }

//         private RolFormPermissionDto MapToDto(RolFormPermission entity)
//         {
//             return new RolFormPermissionDto
//             {
//                 Id = entity.Id,
//                 RolId = entity.RolId,
//                 FormId = entity.FormId,
//                 PermissionId = entity.PermissionId
//             };
//         }
//     }
// }
