// using Business;
// using Entity.DTOs;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Utilities.Exceptions;

// namespace Web.Controllers
// {
//     /// <summary>
//     /// Controlador para la gestión de personas en el sistema
//     /// </summary>
//     [Route("api/[controller]")]
//     [ApiController]
//     [Produces("application/json")]
//     public class PersonController : ControllerBase
//     {
//         private readonly PersonBusiness _personBusiness;
//         private readonly ILogger<PersonController> _logger;

//         public PersonController(PersonBusiness personBusiness, ILogger<PersonController> logger)
//         {
//             _personBusiness = personBusiness;
//             _logger = logger;
//         }

//         [HttpGet]
//         [ProducesResponseType(typeof(IEnumerable<PersonDto>), 200)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> GetAllPeople()
//         {
//             try
//             {
//                 var people = await _personBusiness.GetAllPersonsAsync();
//                 return Ok(people);
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al obtener personas");
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpGet("{id}")]
//         [ProducesResponseType(typeof(PersonDto), 200)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> GetPersonById(int id)
//         {
//             try
//             {
//                 var person = await _personBusiness.GetPersonByIdAsync(id);
//                 return Ok(person);
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida para la persona con ID: {PersonId}", id);
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "Persona no encontrada con ID: {PersonId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al obtener persona con ID: {PersonId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         // [HttpPost]
//         // [ProducesResponseType(typeof(PersonDto), 201)]
//         // [ProducesResponseType(400)]
//         // [ProducesResponseType(500)]
//         // public async Task<IActionResult> CreatePerson([FromBody] PersonDto personDto)
//         // {
//         //     try
//         //     {
//         //         var createdPerson = await _personBusiness.CreatePersonAsync(personDto);
//         //         return CreatedAtAction(nameof(GetPersonById), new { id = createdPerson.Id }, createdPerson);
//         //     }
//         //     catch (ValidationException ex)
//         //     {
//         //         _logger.LogWarning(ex, "Validación fallida al crear persona");
//         //         return BadRequest(new { message = ex.Message });
//         //     }
//         //     catch (ExternalServiceException ex)
//         //     {
//         //         _logger.LogError(ex, "Error al crear persona");
//         //         return StatusCode(500, new { message = ex.Message });
//         //     }
//         // }

//         [HttpPut("{id}")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonDto personDto)
//         {
//             if (id != personDto.Id)
//                 return BadRequest(new { message = "El ID de la persona no coincide con el del objeto." });

//             try
//             {
//                 await _personBusiness.UpdatePersonAsync(personDto);
//                 return NoContent();
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida al actualizar persona con ID: {PersonId}", id);
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "Persona no encontrada con ID: {PersonId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar persona con ID: {PersonId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }

//         [HttpPatch("{id}")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(400)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> PatchPerson(int id, [FromBody] PersonDto personDto)
//         {
//             if (id != personDto.Id)
//                 return BadRequest(new { message = "El ID de la persona no coincide con el del objeto." });

//             try
//             {
//                 await _personBusiness.PatchPersonAsync(personDto);
//                 return NoContent();
//             }
//             catch (ValidationException ex)
//             {
//                 _logger.LogWarning(ex, "Validación fallida al actualizar parcialmente la persona con ID: {PersonId}", id);
//                 return BadRequest(new { message = ex.Message });
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "Persona no encontrada con ID: {PersonId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al actualizar parcialmente la persona con ID: {PersonId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }


//         [HttpDelete("{id}")]
//         [ProducesResponseType(204)]
//         [ProducesResponseType(404)]
//         [ProducesResponseType(500)]
//         public async Task<IActionResult> DeletePerson(int id)
//         {
//             try
//             {
//                 await _personBusiness.DeletePersonAsync(id);
//                 return NoContent();
//             }
//             catch (EntityNotFoundException ex)
//             {
//                 _logger.LogInformation(ex, "Persona no encontrada para eliminación con ID: {PersonId}", id);
//                 return NotFound(new { message = ex.Message });
//             }
//             catch (ExternalServiceException ex)
//             {
//                 _logger.LogError(ex, "Error al eliminar persona con ID: {PersonId}", id);
//                 return StatusCode(500, new { message = ex.Message });
//             }
//         }
//     }
// }
