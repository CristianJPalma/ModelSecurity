// using Data;
// using Entity.DTOs;
// using Entity.Model;
// using Microsoft.Extensions.Logging;
// using Utilities.Exceptions;

// namespace Business
// {
//     /// <summary>
//     /// Clase de negocio encargada de la lógica relacionada con los formModules del sistema.
//     /// </summary>
//     public class FormModuleBusiness
//     {
//         private readonly FormModuleData _formModuleData;
//         private readonly ILogger<FormModuleBusiness> _logger;

//         public FormModuleBusiness(FormModuleData formModuleData, ILogger<FormModuleBusiness> logger)
//         {
//             _formModuleData = formModuleData;
//             _logger = logger;
//         }

//         public async Task<IEnumerable<FormModuleDto>> GetAllFormModulesAsync()
//         {
//             try
//             {
//                 var formModules = await _formModuleData.GetAllAsync();
//                 return formModules.Select(MapToDto);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener todos los formModules");
//                 throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de formModules", ex);
//             }
//         }

//         public async Task<FormModuleDto> GetFormModuleByIdAsync(int id)
//         {
//             if (id <= 0)
//             {
//                 _logger.LogWarning("Se intentó obtener un formModule con ID inválido: {FormModuleId}", id);
//                 throw new ValidationException("id", "El ID del formModule debe ser mayor que cero");
//             }

//             try
//             {
//                 var formModule = await _formModuleData.GetByIdAsync(id);
//                 if (formModule == null)
//                 {
//                     _logger.LogInformation("No se encontró ningún formModule con ID: {FormModuleId}", id);
//                     throw new EntityNotFoundException("FormModule", id);
//                 }

//                 return MapToDto(formModule);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener el formModule con ID: {FormModuleId}", id);
//                 throw new ExternalServiceException("Base de datos", $"Error al recuperar el formModule con ID {id}", ex);
//             }
//         }

//         public async Task<FormModuleDto> CreateFormModuleAsync(FormModuleDto formModuleDto)
//         {
//             try
//             {
//                 ValidateFormModule(formModuleDto);
//                 var formModule = new FormModule
//                 {
//                     ModuleId = formModuleDto.ModuleId,
//                     FormId = formModuleDto.FormId
//                 };

//                 var formModuleCreado = await _formModuleData.CreateAsync(formModule);
//                 return MapToDto(formModuleCreado);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al crear nuevo formModule");
//                 throw new ExternalServiceException("Base de datos", "Error al crear el formModule", ex);
//             }
//         }

//         public async Task UpdateFormModuleAsync(FormModuleDto formModuleDto)
//         {
//             if (formModuleDto == null || formModuleDto.Id <= 0)
//                 throw new ValidationException("Id", "El formulario a actualizar debe tener un ID válido");

//             ValidateFormModule(formModuleDto);

//             try
//             {
//                 var existing = await _formModuleData.GetByIdAsync(formModuleDto.Id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("FormModule", formModuleDto.Id);

//                 existing.FormId = formModuleDto.FormId;
//                 existing.ModuleId = formModuleDto.ModuleId;

//                 var result = await _formModuleData.UpdateAsync(existing);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "Error al actualizar el formModule");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar formulario con ID: {FormId}", formModuleDto.Id);
//                 throw;
//             }
//         }
        
//         /// <summary>
//         /// Actualiza parcialmente un formModule mediante un DTO.
//         /// </summary>
//         public async Task PatchFormModuleAsync(FormModuleDto formModuleDto)
//         {
//             if (formModuleDto == null || formModuleDto.Id <= 0)
//                 throw new ValidationException("Id", "El formModule a actualizar debe tener un ID válido");

//             try
//             {
//                 var existing = await _formModuleData.GetByIdAsync(formModuleDto.Id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("FormModule", formModuleDto.Id);

//                 if (formModuleDto.FormId != 0)
//                     existing.FormId = formModuleDto.FormId;

//                 if (formModuleDto.ModuleId != 0)
//                     existing.ModuleId = formModuleDto.ModuleId;

//                 var result = await _formModuleData.UpdateAsync(existing);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "Error al actualizar el formModule");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar parcialmente el formModule con ID: {formModuleId}", formModuleDto.Id);
//                 throw;
//             }
//         }
//         /// <summary>
//         /// Realiza una eliminación total del FormModule.
//         /// </summary>
//         public async Task DeleteFormModuleAsync(int id)
//         {
//             if (id <= 0)
//                 throw new ValidationException("id", "El ID del FormModule debe ser mayor que cero");

//             try
//             {
//                 var existing = await _formModuleData.GetByIdAsync(id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("formModule", id);

//                 var result = await _formModuleData.DeleteAsync(id);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "No se pudo eliminar el formModule");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al eliminar formModule con ID: {formModuleId}", id);
//                 throw;
//             }
//         }
//         private void ValidateFormModule(FormModuleDto formModuleDto)
//         {
//             if(formModuleDto == null)
//             {
//                 throw new ValidationException("formModule", "El objeto formModule no puede ser nulo");
//             }
//         }
//         private FormModuleDto MapToDto(FormModule formModule)
//         {
//             return new FormModuleDto
//             {
//                 Id = formModule.Id,
//                 ModuleId = formModule.ModuleId,
//                 FormId = formModule.FormId
//             };
//         }
//     }
// }