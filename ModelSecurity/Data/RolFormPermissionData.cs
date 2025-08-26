// using Entity.Context;
// using Entity.Model;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;

// namespace Data
// {
//     public class RolFormPermissionData
//     {
//         private readonly ApplicationDbContext _context;
//         private readonly ILogger<RolFormPermissionData> _logger;

//         public RolFormPermissionData(ApplicationDbContext context, ILogger<RolFormPermissionData> logger)
//         {
//             _context = context;
//             _logger = logger;
//         }

//         public async Task<RolFormPermission> CreateAsync(RolFormPermission rolFormPermission)
//         {
//             try
//             {
//                 await _context.Set<RolFormPermission>().AddAsync(rolFormPermission);
//                 await _context.SaveChangesAsync();
//                 return rolFormPermission;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al crear RolFormPermission: {ex.Message}");
//                 throw;
//             }
//         }

//         public async Task<IEnumerable<RolFormPermission>> GetAllAsync()
//         {
//             return await _context.Set<RolFormPermission>().ToListAsync();
//         }

//         public async Task<RolFormPermission?> GetByIdAsync(int id)
//         {
//             try
//             {
//                 return await _context.Set<RolFormPermission>().FindAsync(id);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener RolFormPermission con ID {Id}", id);
//                 throw;
//             }
//         }

//         public async Task<bool> UpdateAsync(RolFormPermission rolFormPermission)
//         {
//             try
//             {
//                 _context.Set<RolFormPermission>().Update(rolFormPermission);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al actualizar RolFormPermission: {ex.Message}");
//                 return false;
//             }
//         }

//         public async Task<bool> DeleteAsync(int id)
//         {
//             try
//             {
//                 var entity = await _context.Set<RolFormPermission>().FindAsync(id);
//                 if (entity == null)
//                     return false;

//                 _context.Set<RolFormPermission>().Remove(entity);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al eliminar RolFormPermission: {ex.Message}");
//                 return false;
//             }
//         }
//     }
// }
