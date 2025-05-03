using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{    
    /// <summary>
    /// Repositorio encargado de la gestión de la entidad Rol en la base de datos
    /// </summary>
    public class RolData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolData> _logger;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos
        /// </summary>
        /// <param name="context">Instancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos</param>
        public RolData(ApplicationDbContext context, ILogger<RolData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Crea un nuevo rol en la base de datos
        /// </summary>
        /// <param name="rol">Instancia del rol a crear</param>
        /// <returns>El rol creado</returns>
        public async Task<Rol> CreateAsync(Rol rol)
        {
            try
            {
                await _context.Set<Rol>().AddAsync(rol);
                await _context.SaveChangesAsync();
                return rol;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el rol: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los roles almacenados en la base de datos
        /// </summary>
        /// <returns>Lista de los roles</returns>
        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _context.Set<Rol>().ToListAsync();
        }

        /// <summary>Obtiene un rol específico por su identificador</summary>
        public async Task<Rol?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<Rol>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener rol con ID {RolId}", id);
                throw;
            }
        }

        /// <summary>
        /// Actualiza un rol existente en la base de datos
        /// </summary>
        /// <param name="rol">Objeto con la información actualizada</param>
        /// <returns>True si la operación fue exitosa, false en caso contrario</returns>
        public async Task<bool> UpdateAsync(Rol rol)
        {
            try
            {
                _context.Set<Rol>().Update(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el rol: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza parcialmente los campos de un rol existente.
        /// </summary>
        /// <param name="rolId">ID del rol</param>
        /// <param name="rol">Instancia con los nuevos datos</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
        public async Task<bool> PatchAsync(int rolId, Rol rol)
        {
            try
            {
                var existingRol = await _context.Set<Rol>().FindAsync(rolId);
                if (existingRol == null)
                {
                    _logger.LogError($"Rol con ID {rolId} no encontrado.");
                    return false;
                }

                if (rol.Name != null)
                    existingRol.Name = rol.Name;

                if (rol.Description != null)
                    existingRol.Description = rol.Description;

                if (rol.Active != null)
                    existingRol.Active = rol.Active;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el rol: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Realiza una eliminación lógica del rol, marcándolo como inactivo.
        /// </summary>
        /// <param name="id">ID del rol a desactivar</param>
        /// <returns>True si se desactivó correctamente, false si no se encontró</returns>
        public async Task<bool> DisableAsync(int id)
        {
            try
            {
                var rol = await _context.Set<Rol>().FindAsync(id);
                if (rol == null)
                    return false;

                rol.Active = false;
                _context.Set<Rol>().Update(rol); 
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar eliminación lógica del rol con ID {RolId}", id);
                return false;
            }
        }

        /// <summary>
        /// Elimina un rol de la base de datos
        /// </summary>
        /// <param name="id">Identificador único del rol a eliminar</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rol = await _context.Set<Rol>().FindAsync(id);
                if (rol == null)
                    return false;

                _context.Set<Rol>().Remove(rol);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el rol: {ex.Message}");
                return false;
            }
        }
    }
}
