// using Business;
// using Entity.Context;
// using Entity.DTOs;
// using Entity.Model;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Utilities.Exceptions;

// namespace Web.Controllers
// {   
//     [Route("api/[controller]")]
//     [ApiController]
//     [Produces("application/json")]
//     public class AuthController : ControllerBase
//     {
//         private readonly ApplicationDbContext _context;

//         public AuthController(ApplicationDbContext context)
//         {
//             _context = context;
//         }

//         [HttpPost("register")]
//         public async Task<IActionResult> Register([FromBody] RegisterDto dto)
//         {
//             // 1. Crear la persona
//             var person = new Person
//             {
//                 FirstName = dto.FirstName,
//                 LastName = dto.LastName,
//                 Email = dto.Email
//             };

//             _context.Person.Add(person);
//             await _context.SaveChangesAsync();

//             // 2. Crear el usuario
//             var user = new User
//             {
//                 UserName = dto.UserName,
//                 Password = dto.Password, // más adelante: hasheado
//                 Code = $"User{person.Id}",
//                 PersonId = person.Id,
//                 Active = true
//             };

//             _context.User.Add(user);
//             await _context.SaveChangesAsync();

//             // 3. Asignar rol por defecto (opcional)
//             var defaultRol = new RolUser
//             {
//                 UserId = user.Id,
//                 RolId = 1 // Admin por ahora, luego cambias
//             };

//             _context.RolUser.Add(defaultRol);
//             await _context.SaveChangesAsync();

//             return Ok(new { message = "Usuario registrado correctamente" });
//         }
//     }

// }
