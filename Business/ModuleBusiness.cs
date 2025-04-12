using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los modulos del sistema.
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

        // Método para obtener todos los modulos como DTOs
        public async Task<IEnumerable<ModuleDto>> GetAllModuleAsync()
        {
            try
            {
                var modules = await _moduleData.GetAllAsync();
                var modulesDTO = new List<ModuleDto>();

                foreach (var module in modules)
                {
                    modulesDTO.Add(new ModuleDto
                    {
                        Id = module.Id,
                        Name = module.Name,
                        Active = module.Active 
                    });
                }

                return modulesDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los modulos");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de modulos", ex);
            }
        }

        // Método para obtener un modulo por ID como DTO
        public async Task<ModuleDto> GetModuleByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un modulo con ID inválido: {ModuleId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del modulo debe ser mayor que cero");
            }

            try
            {
                var module = await _moduleData.GetByIdAsync(id);
                if (module == null)
                {
                    _logger.LogInformation("No se encontró ningún modulo con ID: {ModuleId}", id);
                    throw new EntityNotFoundException("Modulo", id);
                }

                return new ModuleDto
                {
                    Id = module.Id,
                    Name = module.Name,
                    Active = module.Active
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el modulo con ID: {ModuleId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el modulo con ID {id}", ex);
            }
        }

        // Método para crear un modulo desde un DTO
        public async Task<ModuleDto> CreateModuleAsync(ModuleDto moduleDto)
        {
            try
            {
                ValidateModule(moduleDto);

                var module = new Module
                {
                    Name = moduleDto.Name,
                    Active = moduleDto.Active // Si existe en la entidad
                };
                module.CreateAt = DateTime.Now;
                var moduleCreado = await _moduleData.CreateAsync(module);

                return new ModuleDto
                {
                    Id = moduleCreado.Id,
                    Name = moduleCreado.Name,
                    Active = moduleCreado.Active // Si existe en la entidad
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo modulo: {ModuleNombre}", moduleDto?.Name ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el modulo", ex);
            }
        }

        // Método para validar el DTO
        private void ValidateModule(ModuleDto moduleDto)
        {
            if (moduleDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto modulo no puede ser nulo");
            }

            if (string.IsNullOrWhiteSpace(moduleDto.Name))
            {
                _logger.LogWarning("Se intentó crear/actualizar un modulo con Name vacío");
                throw new Utilities.Exceptions.ValidationException("Name", "El Name del modulo es obligatorio");
            }
        }
        // Método para mapear de Module a ModuleDTO
        private ModuleDto MapToDTO(Module Module)
        {
            return new ModuleDto
            {
                Id = Module.Id,
                Name = Module.Name,
                Active = Module.Active
            };
        }

        //Metodo para mapear de ModuleDTO a Module
        private Module MapToEntity(ModuleDto ModuleDTO)
        {
            return new Module
            {
                Id = ModuleDTO.Id,
                Name = ModuleDTO.Name,
                Active = ModuleDTO.Active

            };
        }
        // Método para mapear una lista de Module a una lista de ModuleDTO
        private IEnumerable<ModuleDto> MapToDTOList(IEnumerable<Module> Modules)
        {
            var ModulesDTO = new List<ModuleDto>();
            foreach (var Module in Modules)
            {
                ModulesDTO.Add(MapToDTO(Module));
            }
            return ModulesDTO;
        }
    }
}