using AutoMapper;
using Data.UnitOfWork;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using System;
namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los módulos del sistema.
    /// </summary>
    public class ModuleBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ModuleBusiness> _logger;
        private readonly IMapper _mapper;
        private readonly IModuleValidationStrategy _validationStrategy;

        public ModuleBusiness(
            IUnitOfWork unitOfWork,
            ILogger<ModuleBusiness> logger,
            IMapper mapper,
            IModuleValidationStrategy validationStrategy)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _validationStrategy = validationStrategy;
        }

        // Método para obtener todos los módulos como DTOs
        public async Task<IEnumerable<ModuleDto>> GetAllModuleAsync()
        {
            try
            {
                var modules = await _unitOfWork.Modules.GetAllAsync();
                return _mapper.Map<IEnumerable<ModuleDto>>(modules);
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
                var module = await _unitOfWork.Modules.GetByIdAsync(id);
                if (module == null)
                {
                    _logger.LogInformation("No se encontró ningún módulo con ID: {ModuleId}", id);
                    throw new EntityNotFoundException("Modulo", id);
                }

                return _mapper.Map<ModuleDto>(module);
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
                _validationStrategy.Validate(moduleDto);
                var module = _mapper.Map<Module>(moduleDto);
                module.CreateAt = DateTime.Now;

                await _unitOfWork.Modules.AddAsync(module);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<ModuleDto>(module);
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

            _validationStrategy.Validate(moduleDto);

            try
            {
                var existing = await _unitOfWork.Modules.GetByIdAsync(moduleDto.Id);
                if (existing == null)
                    throw new EntityNotFoundException("Module", moduleDto.Id);
                
                _mapper.Map(moduleDto, existing);
                _unitOfWork.Modules.Update(existing);
                await _unitOfWork.SaveAsync();
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
                var existing = await _unitOfWork.Modules.GetByIdAsync(moduleDto.Id);
                if (existing == null)
                    throw new EntityNotFoundException("Module", moduleDto.Id);

                // Actualizar solo los campos que se pasen en el DTO (moduleDto)
                // En un PATCH, no se espera que todos los campos estén presentes
                if (!string.IsNullOrEmpty(moduleDto.Name))
                    existing.Name = moduleDto.Name;

                if (moduleDto.Active != null)
                    existing.Active = moduleDto.Active;


                // Intentar actualizar en la base de datos
                _unitOfWork.Modules.Update(existing);
                await _unitOfWork.SaveAsync();
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
                var result = await _unitOfWork.Modules.DisableAsync(id);
                if (!result)
                    throw new ExternalServiceException("Base de datos", "No se pudo desactivar el Modulo");

                await _unitOfWork.SaveAsync();
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
                var existing = await _unitOfWork.Modules.GetByIdAsync(id);
                if (existing == null)
                    throw new EntityNotFoundException("Module", id);

                _unitOfWork.Modules.Delete(existing);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar modulo con ID: {ModuleId}", id);
                throw;
            }
        }
    }
}