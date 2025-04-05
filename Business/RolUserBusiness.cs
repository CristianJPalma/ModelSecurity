using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los rolUsers del sistema.
    /// </summary>
    public class RolUserBusiness
    {
        private readonly RolUserData _rolUserData;
        private readonly ILogger _logger;

        public RolUserBusiness(RolUserData rolUserData, ILogger logger)
        {
            _rolUserData = rolUserData;
            _logger = logger;
        }

        // Método para obtener todos los rolUsers como DTOs
        public async Task<IEnumerable<RolUserDto>> GetAllRolUsersAsync()
        {
            try
            {
                var rolUsers = await _rolUserData.GetAllAsync();
                var rolUsersDto = new List<RolUserDto>();

                foreach (var rolUser in rolUsers)
                {
                    rolUsersDto.Add(new RolUserDto
                    {
                        Id = rolUser.Id,
                        UserId = rolUser.UserId,
                        RolId = rolUser.RolId
                    });
                }

                return rolUsersDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los rolUsers");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de rolUsers", ex);
            }
        }

        // Método para obtener un rolUser por ID como DTO
        public async Task<RolUserDto> GetRolUserByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se intentó obtener un rolUser con ID inválido: {RolUserId}", id);
                throw new Utilities.Exceptions.ValidationException("id", "El ID del rolUser debe ser mayor que cero");
            }

            try
            {
                var rolUser = await _rolUserData.GetByIdAsync(id);
                if (rolUser == null)
                {
                    _logger.LogInformation("No se encontró ningún rolUser con ID: {RolUserId}", id);
                    throw new EntityNotFoundException("RolUser", id);
                }

                return new RolUserDto
                {
                    Id = rolUser.Id,
                    UserId = rolUser.UserId,
                    RolId = rolUser.RolId
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rolUser con ID: {RolUserId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el rolUser con ID {id}", ex);
            }
        }

        // Método para crear un rol desde un DTO
        public async Task<RolUserDto> CreateRolAsync(RolUserDto RolUserDto)
        {
            try
            {
                ValidateRolUser(RolUserDto);

                var rol = new RolUser
                {
                    UserId = RolUserDto.UserId,
                    RolId = RolUserDto.RolId
                };

                var rolUserCreado = await _rolUserData.CreateAsync(rol);

                return new RolUserDto
                {
                    Id = rolUserCreado.Id,
                    UserId = rolUserCreado.UserId,
                    RolId = rolUserCreado.RolId // Si existe en la entidad
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear nuevo formModule: {FormModuleRolId}", RolUserDto.RolId <= 0);
                _logger.LogError(ex, "Error al crear nuevo formModule: {FormModuleUserId}", RolUserDto.UserId <= 0);
                throw new ExternalServiceException("Base de datos", "Error al crear el rolUser", ex);
            }
        }

        // Método para validar el DTO
        private void ValidateRolUser(RolUserDto RolUserDto)
        {
            if (RolUserDto == null)
            {
                throw new Utilities.Exceptions.ValidationException("El objeto rolUser no puede ser nulo");
            }

            if (RolUserDto.RolId <= 0)
            {
                _logger.LogWarning("Se intentó crear/actualizar un rolUser con RolId vacío");
                throw new Utilities.Exceptions.ValidationException("RolId", "El RolId del rol es obligatorio");
            }
        }
        // Método para mapear de RolUser a RolUserDTO
        private RolUserDto MapToDTO(RolUser rolUser)
        {
            return new RolUserDto
            {
                Id = rolUser.Id,
                RolId = rolUser.RolId,
                UserId = rolUser.UserId
            };
        }

        //Metodo para mapear de RolUserDTO a RolUser
        private RolUser MapToEntity(RolUserDto rolUserDTO)
        {
            return new RolUser
            {
                Id = rolUserDTO.Id,
                RolId = rolUserDTO.RolId,
                UserId = rolUserDTO.UserId

            };
        }
        // Método para mapear una lista de Rolser a una lista de RolUserDTO
        private IEnumerable<RolUserDto> MapToDTOList(IEnumerable<RolUser> rolUsers)
        {
            var rolUsersDTO = new List<RolUserDto>();
            foreach (var rolUser in rolUsers)
            {
                rolUsersDTO.Add(MapToDTO(rolUser));
            }
            return rolUsersDTO;
        }
    }
}