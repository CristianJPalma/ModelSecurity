using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;

namespace Business
{
    /// <summary>
    /// Clase de negocio encargada de la lógica relacionada con los permisos del sistema.
    /// </summary>
    public class PermissionBusiness
    {
        private readonly PermissionData _permissionData;
        private readonly ILogger<PermissionBusiness> _logger;

        public PermissionBusiness(PermissionData permissionData, ILogger<PermissionBusiness> logger)
        {
            _permissionData = permissionData;
            _logger = logger;
        }

        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
            try
            {
                var permissions = await _permissionData.GetAllAsync();
                return permissions.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todos los permisos");
                throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de permisos", ex);
            }
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID del permiso debe ser mayor que cero");

            try
            {
                var permission = await _permissionData.GetByIdAsync(id);
                if (permission == null)
                    throw new EntityNotFoundException("Permission", id);

                return MapToDto(permission);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el permiso con ID: {PermissionId}", id);
                throw new ExternalServiceException("Base de datos", $"Error al recuperar el permiso con ID {id}", ex);
            }
        }

        public async Task<PermissionDto> CreatePermissionAsync(PermissionDto permissionDto)
        {
            try
            {
                ValidatePermission(permissionDto);
                var permission = new Permission
                {
                    Name = permissionDto.Name,
                    Code = permissionDto.Code,
                    Active = permissionDto.Active,
                    CreateAt = DateTime.Now
                };

                var created = await _permissionData.CreateAsync(permission);
                return MapToDto(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear permiso: {PermissionName}", permissionDto?.Name ?? "null");
                throw new ExternalServiceException("Base de datos", "Error al crear el permiso", ex);
            }
        }

        public async Task UpdatePermissionAsync(PermissionDto permissionDto)
        {
            if (permissionDto == null || permissionDto.Id <= 0)
                throw new ValidationException("Id", "El permiso a actualizar debe tener un ID válido");

            ValidatePermission(permissionDto);

            try
            {
                var existing = await _permissionData.GetByIdAsync(permissionDto.Id);
                if (existing == null)
                    throw new EntityNotFoundException("Permission", permissionDto.Id);

                existing.Name = permissionDto.Name;
                existing.Code = permissionDto.Code;
                existing.Active = permissionDto.Active;

                var result = await _permissionData.UpdateAsync(existing);
                if (!result)
                    throw new ExternalServiceException("Base de datos", "Error al actualizar el permiso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar permiso con ID: {PermissionId}", permissionDto.Id);
                throw;
            }
        }

        /// <summary>
        /// Actualiza parcialmente un permiso mediante un DTO.
        /// </summary>
        public async Task PatchPermissionAsync(PermissionDto permissionDto)
        {
            if (permissionDto == null || permissionDto.Id <= 0)
                throw new ValidationException("Id", "El permiso a actualizar debe tener un ID válido");

            try
            {
                var existing = await _permissionData.GetByIdAsync(permissionDto.Id);
                if (existing == null)
                    throw new EntityNotFoundException("Permission", permissionDto.Id);

                if (!string.IsNullOrEmpty(permissionDto.Name))
                    existing.Name = permissionDto.Name;

                if (!string.IsNullOrEmpty(permissionDto.Code))
                    existing.Code = permissionDto.Code;

                if (permissionDto.Active != null)
                    existing.Active = permissionDto.Active;

                var result = await _permissionData.UpdateAsync(existing);
                if (!result)
                    throw new ExternalServiceException("Base de datos", "Error al actualizar el permiso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar parcialmente el permiso con ID: {PermissionId}", permissionDto.Id);
                throw;
            }
        }

        /// <summary>
        /// Realiza una eliminación lógica del permiso.
        /// </summary>
        public async Task DisablePermissionAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID del permiso debe ser mayor que cero");

            try
            {
                var existing = await _permissionData.GetByIdAsync(id);
                if (existing == null)
                    throw new EntityNotFoundException("Permission", id);

                var result = await _permissionData.DisableAsync(id);
                if (!result)
                    throw new ExternalServiceException("Base de datos", "No se pudo desactivar el permiso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al desactivar permiso con ID: {PermissionId}", id);
                throw;
            }
        }

        /// <summary>
        /// Realiza una eliminación total del permiso.
        /// </summary>
        public async Task DeletePermissionAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("id", "El ID del permiso debe ser mayor que cero");

            try
            {
                var existing = await _permissionData.GetByIdAsync(id);
                if (existing == null)
                    throw new EntityNotFoundException("Permission", id);

                var result = await _permissionData.DeleteAsync(id);
                if (!result)
                    throw new ExternalServiceException("Base de datos", "No se pudo eliminar el permiso");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar permiso con ID: {PermissionId}", id);
                throw;
            }
        }

        private void ValidatePermission(PermissionDto permissionDto)
        {
            if (permissionDto == null)
                throw new ValidationException("permissionDto", "El objeto permiso no puede ser nulo");

            if (string.IsNullOrWhiteSpace(permissionDto.Code))
                throw new ValidationException("Code", "El campo 'Code' del permiso es obligatorio");
        }

        private PermissionDto MapToDto(Permission permission)
        {
            return new PermissionDto
            {
                Id = permission.Id,
                Name = permission.Name,
                Code = permission.Code,
                Active = permission.Active
            };
        }
    }
}
