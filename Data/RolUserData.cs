using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data
{
    /// <summary>
    /// Repositorio encargado de la gestión de la entidad RolUser en la base de datos
    /// </summary>
    public class RolUserData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RolUserData> _logger;

        /// <summary>
        /// Constructor que recibe el contexto de la base de datos
        /// </summary>
        /// <param name="context">Instancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos</param>
        public RolUserData(ApplicationDbContext context, ILogger<RolUserData> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Crea un nuevo RolUser en la base de datos
        /// </summary>
        /// <param name="rolUser">Instancia de RolUser a crear</param>
        /// <returns>El RolUser creado</returns>
        public async Task<RolUser> CreateAsync(RolUser rolUser)
        {
            try
            {
                await _context.Set<RolUser>().AddAsync(rolUser);
                await _context.SaveChangesAsync();
                return rolUser;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el RolUser: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los RolUsers almacenados en la base de datos
        /// </summary>
        /// <returns>Lista de los RolUsers</returns>
        public async Task<IEnumerable<RolUser>> GetAllAsync()
        {
            return await _context.Set<RolUser>().ToListAsync();
        }

        /// <summary>
        /// Obtiene un RolUser específico por su identificador
        /// </summary>
        public async Task<RolUser?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<RolUser>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener RolUser con ID {RolUserId}", id);
                throw;
            }
        }

        /// <summary>
        /// Actualiza un RolUser existente en la base de datos
        /// </summary>
        /// <param name="rolUser">Objeto con la información actualizada</param>
        /// <returns>True si la operación fue exitosa, false en caso contrario</returns>
        public async Task<bool> UpdateAsync(RolUser rolUser)
        {
            try
            {
                _context.Set<RolUser>().Update(rolUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el RolUser: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza parcialmente los campos de un RolUser existente
        /// </summary>
        /// <param name="rolUserId">ID del RolUser</param>
        /// <param name="rolUser">Instancia con los nuevos datos</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
        public async Task<bool> PatchAsync(int rolUserId, RolUser rolUser)
        {
            try
            {
                var existingRolUser = await _context.Set<RolUser>().FindAsync(rolUserId);
                if (existingRolUser == null)
                {
                    _logger.LogError($"RolUser con ID {rolUserId} no encontrado.");
                    return false;
                }

                if (rolUser.UserId != 0)
                    existingRolUser.UserId = rolUser.UserId;

                if (rolUser.RolId != 0)
                    existingRolUser.RolId = rolUser.RolId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el RolUser: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un RolUser de la base de datos
        /// </summary>
        /// <param name="id">Identificador único del RolUser a eliminar</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var rolUser = await _context.Set<RolUser>().FindAsync(id);
                if (rolUser == null)
                    return false;

                _context.Set<RolUser>().Remove(rolUser);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al eliminar el RolUser: {ex.Message}");
                return false;
            }
        }
    }
}
