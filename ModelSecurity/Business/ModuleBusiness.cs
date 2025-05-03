using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los módulos del sistema.
    /// </summary>
    public class ModuleBusiness
    {
        private readonly ModuleData _moduleData;
        private readonly ILogger<ModuleBusiness> _logger;

        public ModuleBusiness(ModuleData moduleData, ILogger<ModuleBusiness> logger)
        {
            _moduleData = moduleData;
            _logger = logger;
        }

        // Método para obtener todos los módulos como DTOs
        public async Task<IEnumerable<ModuleDto>> GetAllModuleAsync()
        {
            try
            {
                var modules = await _moduleData.GetAllAsync();
                return modules.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los módulos");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de módulos", ex);
            }
        }

        // Método para obtener un módulo por ID como DTO
        public async Task<ModuleDto> GetModuleByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un módulo con ID inválido: {ModuleId}", id);
                throw new ValidationException("id", "El ID del módulo debe ser mayor que cero");
            }

            try
            {
                var module = await _moduleData.GetByIdAsync(id);
                if (module == null)
                {
                    _logger.LogInformation("No se encontró ningún módulo con ID: {ModuleId}", id);
                    throw new EntityNotFoundException("Modulo", id);
                }

                return MapToDto(module);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el módulo con ID: {ModuleId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el módulo con ID {id}", ex);
            }
        }

        // Método para crear un módulo desde un DTO
        public async Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto)
        {
            try
            {
                ValidateModule(moduleDto);
                var module = new Module
                {
                    Name = moduleDto.Name,
                    Active = moduleDto.Active
                };

                
                 module.CreateAt=DateTime.Now;

                var moduleCreado = await _moduleData.CreateAsync(module);
                return MapToDto(moduleCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear modulo: {ModuleNombre}", moduleDto?.Name ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el modulo", ex);
            }
        }

        // Método para actualizar un módulo existente
        public async Task UpdateModuleAsync(ModuleDto moduleDto)
        {
            if (moduleDto == null || moduleDto.Id <= 0)
                throw new ValidationException("Id", "El modulo a actualizar debe tener un ID válido");

            ValidateModule(moduleDto);

            try
            {
                var existing = await _moduleData.GetByIdAsync(moduleDto.Id);
                if (existing == null)
                    throw new EntityNotFoundException("Module", moduleDto.Id);

                existing.Name = moduleDto.Name;
                existing.Active = moduleDto.Active;

                var result = await _moduleData.UpdateAsync(existing);
                if (!result)
                    throw new ExternalServiceException("Base de datos", "Error al actualizar el modulo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar modulo con ID: {ModuleId}", moduleDto.Id);
                throw;
            }
        }
        /// <summary>
        /// Actualiza parcialmente un modulo mediante un diccionario de campos.
        /// </summary>
        /// <param name="id">ID del modulo a actualizar</param>
        /// <param name="updatedFields">Diccionario con los nombres de los campos y sus nuevos valores</param>
        /// <returns>Task</returns>
        public async Task PatchModuleAsync(ModuleDto moduleDto)
        {
            if (moduleDto == null || moduleDto.Id <= 0)
                throw new ValidationException("Id", "El modulo a actualizar debe tener un ID válido");
            try
            {
                // Buscar el modulo existente por su ID
                var existing = await _moduleData.GetByIdAsync(moduleDto.Id);
                if (existing == null)
                    throw new EntityNotFoundException("Module", moduleDto.Id);

                // Actualizar solo los campos que se pasen en el DTO (moduleDto)
                // En un PATCH, no se espera que todos los campos estén presentes
                if (!string.IsNullOrEmpty(moduleDto.Name))
                    existing.Name = moduleDto.Name;

                if (moduleDto.Active != null)
                    existing.Active = moduleDto.Active;


                // Intentar actualizar en la base de datos
                var result = await _moduleData.UpdateAsync(existing);
                if (!result)
                    throw new ExternalServiceException("Base de datos", "Error al actualizar el modulo");
        }
        catch (Exception ex)
        {
            // Manejo de errores
            _logger.LogError(ex, "Error al actualizar parcialmente el modulo con ID: {ModuleId}", moduleDto.Id);
            throw;
        }
    }


        /// <summary>
        /// Realiza una eliminación lógica del modulo.
        /// </summary>
        /// <param name="id">ID del modulo</param>
        public async Task DisableModuleAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID del modulo debe ser mayor que cero");

            try
            {
                var existing = await _moduleData.GetByIdAsync(id);
                if (existing == null)
                    throw new EntityNotFoundException("Module", id);

                var result = await _moduleData.DisableAsync(id);
                if (!result)
                
                    throw new ExternalServiceException("Base de datos", "No se pudo desactivar el modulo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar modulo con ID: {ModuleId}", id);
                throw;
            }
        }
        /// <summary>
        /// Realiza una eliminación total del modulo.
        /// </summary>
        /// <param name="id">ID del modulo</param>
        public async Task DeleteModuleAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID del modulo debe ser mayor que cero");

            try
            {
                var existing = await _moduleData.GetByIdAsync(id);
                if (existing == null)
                    throw new EntityNotFoundException("Module", id);

                var result = await _moduleData.DeleteAsync(id);
                if (!result)
                    throw new ExternalServiceException("Base de datos", "No se pudo eliminar el modulo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar modulo con ID: {ModuleId}", id);
                throw;
            }
        }

        // Método para validar el DTO
        private void ValidateModule(ModuleDto moduleDto)
        {
            if (moduleDto == null)
            {
                throw new ValidationException("El objeto módulo no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(moduleDto.Name))
            {
                throw new ValidationException("Name", "El Name del módulo es obligatorio");
            }
        }

        // Método para mapear un entity a DTO
        private ModuleDto MapToDto(Module module)
        {
            return new ModuleDto
            {
                Id = module.Id,
                Name = module.Name,
                Active = module.Active
            };
        }

    }
}