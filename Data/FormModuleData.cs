using Entity.Context;
using Entity.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Data
{
    /// <summary>
    /// Repositorio encargado de la gestion de la entidad FormModule en la base de datos
    /// </summary>
    public class FormModuleData
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FormModuleData> _logger;

        ///<summary>
        ///Constructor que recibe el contexto de la base de datos
        ///</summary>
        ///<param name="context">Instancia de <see cref="ApplicationDbContext"/>para la conexion con la base de datos</param>
        public FormModuleData(ApplicationDbContext context, ILogger<FormModuleData> logger)
        {
            _context = context;
            _logger = logger;
        }

        ///<summary>
        ///Crea un nuevo FormModule en la base de datos
        ///</summary>
        ///<param name="formModule">Instancia del formModule a crear</param>
        ///<returns>El formModule creado</returns>
        public async Task<FormModule> CreateAsync(FormModule formModule)
        {
            try
            {
                await _context.Set<FormModule>().AddAsync(formModule);
                await _context.SaveChangesAsync();
                return formModule;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al crear el formModule: {ex.Message}");
                throw;
            }
        }

        ///<summary>
        ///Obtiene todos los formModules almacenados en la base de datos
        ///</summary>
        ///<returns>Lista de los formModules</returns>
        public async Task<IEnumerable<FormModule>> GetAllAsync()
        {
            return await _context.Set<FormModule>().ToListAsync();
        }
        ///<summary>Obtiene un FormModule especifico por su identificador</summary>
        public async Task<FormModule?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.Set<FormModule>().FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener FormModule con ID {FormModuleId}", id);
                throw;
            }
        }
        /// <summary>
        /// Actualiza un formModule existente en la base de datos
        /// </summary>
        /// <param name="FormModule">Objeto con la información actualizada</param>
        /// <returns>True si la operación fue exitosa, false en caso contrario</returns>
        public async Task<bool> UpdateAsync(FormModule FormModule)
        {
            try
            {
                _context.Set<FormModule>().Update(FormModule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el FormModule: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza parcialmente los campos de un FormModule existente.
        /// </summary>
        /// <param name="FormModuleId">ID del FormModule</param>
        /// <param name="FormModule">Instancia con los nuevos datos</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
        public async Task<bool> PatchAsync(int FormModuleId, FormModule FormModule)
        {
            try
            {
                var existingFormModule = await _context.Set<FormModule>().FindAsync(FormModuleId);
                if (existingFormModule == null)
                {
                    _logger.LogError($"FormModule con ID {FormModuleId} no encontrado.");
                    return false;
                }

                if (FormModule.ModuleId != null)
                    existingFormModule.ModuleId = FormModule.ModuleId;

                if (FormModule.FormId != null)
                    existingFormModule.FormId = FormModule.FormId;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al actualizar el FormModule: {ex.Message}");
                return false;
            }
        }
         /// <summary>
        /// Elimina un FormModule de la base de datos
        /// </summary>
        /// <param name="id">Identificador único del FormModule a eliminar</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var FormModule = await _context.Set<FormModule>().FindAsync(id);
                if (FormModule == null)
                    return false;

                _context.Set<FormModule>().Remove(FormModule);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar el FormModule: {ex.Message}");
                return false;
            }
        }
    }
}