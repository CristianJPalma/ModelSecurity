// using Entity.Context;
// using Entity.Model;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;

// namespace Data
// {
//     /// <summary>
//     /// Repositorio encargado de la gestion de la entidad Module en la base de datos
//     /// </summary>
//     public class ModuleData
//     {
//         private readonly ApplicationDbContext _context;
//         private readonly ILogger<ModuleData> _logger;

//         ///<summary>
//         ///Constructor que recibe el contexto de la base de datos
//         ///</summary>
//         ///<param name="context">Instancia de <see cref="ApplicationDbContext"/>para la conexion con la base de datos</param>
//         public ModuleData(ApplicationDbContext context, ILogger<ModuleData> logger)
//         {
//             _context = context;
//             _logger = logger;
//         }

//         ///<summary>
//         ///Crea un nuevo modulo en la base de datos
//         ///</summary>
//         ///<param name="module">Instancia del modulo a crear</param>
//         ///<returns>El modulo creado</returns>
//         public async Task<Module> CreateAsync(Module module)
//         {
//             try
//             {
//                 await _context.Set<Module>().AddAsync(module);
//                 await _context.SaveChangesAsync();
//                 return module;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al crear el modulo: {ex.Message}");
//                 throw;
//             }
//         }

//         ///<summary>
//         ///Obtiene todos los modulos almacenados en la base de datos
//         ///</summary>
//         ///<returns>Lista de los modulos</returns>
//         public async Task<IEnumerable<Module>> GetAllAsync()
//         {
//             return await _context.Set<Module>().ToListAsync();
//         }
//         ///<summary>Obtiene un modulo especifico por su identificador</summary>
//         public async Task<Module?> GetByIdAsync(int id)
//         {
//             try
//             {
//                 return await _context.Set<Module>().FindAsync(id);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener modulo con ID {ModuleId}", id);
//                 throw;
//             }
//         }

//         ///<summary>
//         ///Actualiza un Modulo existente en la base de datos
//         ///</summary>
//         ///<param name="module">Objeto con la informacion actualizada</param>
//         ///<returns>True si la operacion fue exitosa, false en caso contrario</returns>
//         public async Task<bool> UpdateAsync(Module module)
//         {
//             try
//             {
//                 _context.Set<Module>().Update(module);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al actualizar el modulo: {ex.Message}");
//                 return false;
//             }
//         }


//         /// <summary>
//         /// Actualiza parcialmente los campos de un modulo existente.
//         /// path
//         /// </summary>
//         /// <param name="module">Diccionario con los nombres de los campos y sus nuevos valores</param>
//         /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
//         public async Task<bool> PatchAsync(int moduleId, Module module)
//         {
//             try
//             {
//                 // Buscar el modulo existente por su ID
//                 var existingModule = await _context.Set<Module>().FindAsync(moduleId);
                
//                 // Si no se encuentra el modulo, retornar false
//                 if (existingModule == null)
//                 {
//                     _logger.LogError($"Modulo con ID {moduleId} no encontrado.");
//                     return false;
//                 }

//                 // Actualizar solo los campos que han sido modificados
//                 if (module.Name != null)
//                     existingModule.Name = module.Name;
                    
//                 if (module.Active != null)
//                     existingModule.Active = module.Active;

//                 // Guardar los cambios
//                 await _context.SaveChangesAsync();

//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al actualizar el modulo: {ex.Message}");
//                 return false;
//             }
//         }


//         /// <summary>
//         /// Realiza una eliminación lógica del modulo, marcándolo como inactivo.
//         /// </summary>
//         /// <param name="id">ID del modulo a desactivar</param>
//         /// <returns>True si se desactivó correctamente, false si no se encontró</returns>
//         public async Task<bool> DisableAsync(int id)
//         {
//             try
//             {
//                 var module = await _context.Set<Module>().FindAsync(id);
//                 if (module == null)
//                     return false;

//                 module.Active = false;
//                 _context.Set<Module>().Update(module); 
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al realizar eliminación lógica del modulo con ID {ModuleId}", id);
//                 return false;
//             }
//         }

//         ///<summary>
//         ///Elimina un modulo de la base de datos
//         ///</summary>
//         ///<param name="id">Identificador unico del modulo a eliminar</param>
//         ///<returns>True si la eliminacion fue exitosa, false en caso contrario</returns>
//         ///
//         public async Task<bool> DeleteAsync(int id)
//         {
//             try
//             {
//                 var module = await _context.Set<Module>().FindAsync(id);
//                 if (module == null)
//                     return false;

//                 _context.Set<Module>().Remove(module);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error al elminar el modulo: {ex.Message}");
//                 return false;
//             }

//         }
//     }
// }
