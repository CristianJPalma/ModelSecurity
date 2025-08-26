// using Entity.Context;
// using Entity.Model;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;

// namespace Data
// {
//     /// <summary>
//     /// Repositorio encargado de la gestión de la entidad Person en la base de datos
//     /// </summary>
//     public class PersonData
//     {
//         private readonly ApplicationDbContext _context;
//         private readonly ILogger<PersonData> _logger;

//         public PersonData(ApplicationDbContext context, ILogger<PersonData> logger)
//         {
//             _context = context;
//             _logger = logger;
//         }

//         public async Task<Person> CreateAsync(Person person)
//         {
//             try
//             {
//                 await _context.Set<Person>().AddAsync(person);
//                 await _context.SaveChangesAsync();
//                 return person;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al crear la persona: {ex.Message}");
//                 throw;
//             }
//         // }

//         public async Task<IEnumerable<Person>> GetAllAsync()
//         {
//             return await _context.Set<Person>().ToListAsync();
//         }

//         public async Task<Person?> GetByIdAsync(int id)
//         {
//             try
//             {
//                 return await _context.Set<Person>().FindAsync(id);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener persona con ID {PersonId}", id);
//                 throw;
//             }
//         }

//         public async Task<bool> UpdateAsync(Person person)
//         {
//             try
//             {
//                 _context.Set<Person>().Update(person);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al actualizar la persona: {ex.Message}");
//                 return false;
//             }
//         }

//         public async Task<bool> PatchAsync(int personId, Person person)
//         {
//             try
//             {
//                 var existing = await _context.Set<Person>().FindAsync(personId);
//                 if (existing == null)
//                 {
//                     _logger.LogError($"Persona con ID {personId} no encontrada.");
//                     return false;
//                 }

//                 if (!string.IsNullOrEmpty(person.FirstName))
//                     existing.FirstName = person.FirstName;

//                 if (!string.IsNullOrEmpty(person.LastName))
//                     existing.LastName = person.LastName;

//                 if (!string.IsNullOrEmpty(person.Email))
//                     existing.Email = person.Email;

//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al actualizar parcialmente la persona: {ex.Message}");
//                 return false;
//             }
//         }



//         public async Task<bool> DeleteAsync(int id)
//         {
//             try
//             {
//                 var person = await _context.Set<Person>().FindAsync(id);
//                 if (person == null)
//                     return false;

//                 _context.Set<Person>().Remove(person);
//                 await _context.SaveChangesAsync();
//                 return true;
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError($"Error al eliminar la persona: {ex.Message}");
//                 return false;
//             }
//         }
//     }
// }
