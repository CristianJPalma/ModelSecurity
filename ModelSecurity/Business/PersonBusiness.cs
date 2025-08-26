// using Data;
// using Entity.DTOs;
// using Entity.Model;
// using Microsoft.Extensions.Logging;
// using Utilities.Exceptions;

// namespace Business
// {
//     public class PersonBusiness
//     {
//         private readonly PersonData _personData;
//         private readonly ILogger<PersonBusiness> _logger;

//         public PersonBusiness(PersonData personData, ILogger<PersonBusiness> logger)
//         {
//             _personData = personData;
//             _logger = logger;
//         }

//         public async Task<IEnumerable<PersonDto>> GetAllPersonsAsync()
//         {
//             try
//             {
//                 var persons = await _personData.GetAllAsync();
//                 return persons.Select(p => new PersonDto
//                 {
//                     Id = p.Id,
//                     FirstName = p.FirstName,
//                     LastName = p.LastName,
//                     Email = p.Email
//                 });
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener todas las personas");
//                 throw new ExternalServiceException("Base de datos", "Error al recuperar la lista de personas", ex);
//             }
//         }

//         public async Task<PersonDto> GetPersonByIdAsync(int id)
//         {
//             if (id <= 0)
//                 throw new ValidationException("id", "El ID de la persona debe ser mayor que cero");

//             try
//             {
//                 var person = await _personData.GetByIdAsync(id);
//                 if (person == null)
//                     throw new EntityNotFoundException("Persona", id);

//                 return new PersonDto
//                 {
//                     Id = person.Id,
//                     FirstName = person.FirstName,
//                     LastName = person.LastName,
//                     Email = person.Email
//                 };
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al obtener la persona con ID: {PersonId}", id);
//                 throw new ExternalServiceException("Base de datos", $"Error al recuperar la persona con ID {id}", ex);
//             }
//         }

//         public async Task<PersonDto> CreatePersonAsync(PersonDto personDto)
//         {
//             try
//             {
//                 ValidatePerson(personDto);
//                 var person = new Person
//                 {
//                     FirstName = personDto.FirstName,
//                     LastName = personDto.LastName,
//                     Email = personDto.Email,
//                 };

//                 var createdPerson = await _personData.CreateAsync(person);
//                 return new PersonDto
//                 {
//                     Id = createdPerson.Id,
//                     FirstName = createdPerson.FirstName,
//                     LastName = createdPerson.LastName,
//                     Email = createdPerson.Email
//                 };
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al crear persona: {PersonName}", personDto?.FirstName ?? "null");
//                 throw new ExternalServiceException("Base de datos", "Error al crear la persona", ex);
//             }
//         }

//         public async Task UpdatePersonAsync(PersonDto personDto)
//         {
//             if (personDto == null || personDto.Id <= 0)
//                 throw new ValidationException("Id", "La persona a actualizar debe tener un ID válido");

//             ValidatePerson(personDto);

//             try
//             {
//                 var existing = await _personData.GetByIdAsync(personDto.Id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("Persona", personDto.Id);

//                 existing.FirstName = personDto.FirstName;
//                 existing.LastName = personDto.LastName;
//                 existing.Email = personDto.Email;

//                 var result = await _personData.UpdateAsync(existing);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "Error al actualizar la persona");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar persona con ID: {PersonId}", personDto.Id);
//                 throw;
//             }
//         }

//         public async Task PatchPersonAsync(PersonDto personDto)
//         {
//             if (personDto == null || personDto.Id <= 0)
//                 throw new ValidationException("Id", "La persona a actualizar debe tener un ID válido");

//             try
//             {
//                 var existing = await _personData.GetByIdAsync(personDto.Id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("Persona", personDto.Id);

//                 if (!string.IsNullOrEmpty(personDto.FirstName))
//                     existing.FirstName = personDto.FirstName;

//                 if (!string.IsNullOrEmpty(personDto.LastName))
//                     existing.LastName = personDto.LastName;

//                 if (!string.IsNullOrEmpty(personDto.Email))
//                     existing.Email = personDto.Email;

//                 var result = await _personData.UpdateAsync(existing);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "Error al actualizar parcialmente la persona");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar parcialmente la persona con ID: {PersonId}", personDto.Id);
//                 throw;
//             }
//         }


//         public async Task DeletePersonAsync(int id)
//         {
//             if (id <= 0)
//                 throw new ValidationException("id", "El ID de la persona debe ser mayor que cero");

//             try
//             {
//                 var existing = await _personData.GetByIdAsync(id);
//                 if (existing == null)
//                     throw new EntityNotFoundException("Persona", id);

//                 var result = await _personData.DeleteAsync(id);
//                 if (!result)
//                     throw new ExternalServiceException("Base de datos", "No se pudo eliminar la persona");
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error al eliminar persona con ID: {PersonId}", id);
//                 throw;
//             }
//         }

//         private void ValidatePerson(PersonDto personDto)
//         {
//             if (personDto == null)
//                 throw new ValidationException("personDto", "El objeto persona no puede ser nulo");

//             if (string.IsNullOrWhiteSpace(personDto.FirstName))
//                 throw new ValidationException("FirstName", "El nombre es obligatorio");

//             if (string.IsNullOrWhiteSpace(personDto.LastName))
//                 throw new ValidationException("LastName", "El apellido es obligatorio");

//             if (string.IsNullOrWhiteSpace(personDto.Email))
//                 throw new ValidationException("Email", "El correo electrónico es obligatorio");
//         }
//     }
// }
