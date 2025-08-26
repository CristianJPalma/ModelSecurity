// using Entity.Context;
// using Entity.Model;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;

// namespace Data
// {
//     /// <summary>
//     /// Repositorio encargado de la gestión de la entidad Permission en la base de datos
//     /// </summary>
//     public class PermissionData
//     {
//         private readonly ApplicationDbContext _context;
//         private readonly ILogger<PermissionData> _logger;

//         /// <summary>
//         /// Constructor que recibe el contexto de la base de datos
//         /// </summary>
//         /// <param name="context">Instancia de <see cref="ApplicationDbContext"/> para la conexión con la base de datos</param>
//         public PermissionData(ApplicationDbContext context, ILogger<PermissionData> logger)
//         {
//             _context = context;
//             _logger = logger;
//         }

//         /// <summary>
//         /// Crea un nuevo permiso en la base de datos
//         /// </summary>
//         /// <param name="permission">Instancia del permiso a crear</param>
//         /// <returns>El permiso creado</returns>
//         public async Task<Permission> CreateAsync(Permission permission)
//         {
//             try
//             {
//                 await _context.Set<Permission>().AddAsync(permission);
//                 await _context.SaveChangesAsync();
//                 return permission;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al crear el permiso: {ex.Message}");
//                 throw;
//             }
//         }

//         /// <summary>
//         /// Obtiene todos los permisos almacenados en la base de datos
//         /// </summary>
//         /// <returns>Lista de los permisos</returns>
//         public async Task<IEnumerable<Permission>> GetAllAsync()
//         {
//             return await _context.Set<Permission>().ToListAsync();
//         }

//         /// <summary>Obtiene un permiso específico por su identificador</summary>
//         public async Task<Permission?> GetByIdAsync(int id)
//         {
//             try
//             {
//                 return await _context.Set<Permission>().FindAsync(id);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener permiso con ID {PermissionId}", id);
//                 throw;
//             }
//         }

//         /// <summary>
//         /// Actualiza un permiso existente en la base de datos
//         /// </summary>
//         /// <param name="permission">Objeto con la información actualizada</param>
//         /// <returns>True si la operación fue exitosa, false en caso contrario</returns>
//         public async Task<bool> UpdateAsync(Permission permission)
//         {
//             try
//             {
//                 _context.Set<Permission>().Update(permission);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al actualizar el permiso: {ex.Message}");
//                 return false;
//             }
//         }

//         /// <summary>
//         /// Actualiza parcialmente los campos de un permiso existente.
//         /// </summary>
//         /// <param name="permissionId">ID del permiso</param>
//         /// <param name="permission">Instancia con los nuevos datos</param>
//         /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
//         public async Task<bool> PatchAsync(int permissionId, Permission permission)
//         {
//             try
//             {
//                 var existingPermission = await _context.Set<Permission>().FindAsync(permissionId);
//                 if (existingPermission == null)
//                 {
//                     _logger.LogError($"Permiso con ID {permissionId} no encontrado.");
//                     return false;
//                 }

//                 if (permission.Name != null)
//                     existingPermission.Name = permission.Name;

//                 if (permission.Code != null)
//                     existingPermission.Code = permission.Code;

//                 if (permission.Active != null)
//                     existingPermission.Active = permission.Active;

//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al actualizar el permiso: {ex.Message}");
//                 return false;
//             }
//         }

//         /// <summary>
//         /// Realiza una eliminación lógica del permiso, marcándolo como inactivo.
//         /// </summary>
//         /// <param name="id">ID del permiso a desactivar</param>
//         /// <returns>True si se desactivó correctamente, false si no se encontró</returns>
//         public async Task<bool> DisableAsync(int id)
//         {
//             try
//             {
//                 var permission = await _context.Set<Permission>().FindAsync(id);
//                 if (permission == null)
//                     return false;

//                 permission.Active = false;
//                 permission.DeleteAt = DateTime.Now;
//                 _context.Set<Permission>().Update(permission);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al realizar eliminación lógica del permiso con ID {PermissionId}", id);
//                 return false;
//             }
//         }

//         /// <summary>
//         /// Elimina un permiso de la base de datos
//         /// </summary>
//         /// <param name="id">Identificador único del permiso a eliminar</param>
//         /// <returns>True si la eliminación fue exitosa, false en caso contrario</returns>
//         public async Task<bool> DeleteAsync(int id)
//         {
//             try
//             {
//                 var permission = await _context.Set<Permission>().FindAsync(id);
//                 if (permission == null)
//                     return false;

//                 _context.Set<Permission>().Remove(permission);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error al eliminar el permiso: {ex.Message}");
//                 return false;
//             }
//         }
//     }
// }
