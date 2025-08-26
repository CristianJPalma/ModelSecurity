// using Business;
// using Entity.DTOs;
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
//     public class LoginController : ControllerBase
//     {
//         private readonly LoginBusiness _loginBusiness;

//         public LoginController(LoginBusiness loginBusiness)
//         {
//             _loginBusiness = loginBusiness;
//         }

//         /// <summary>
//         /// Endpoint para login de usuario. Recibe email y contraseña.
//         /// </summary>
//         [HttpPost]
//         public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
//         {
//             try
//             {
//                 var result = await _loginBusiness.LoginAsync(loginDto);

//                 return Ok(result); // Ahora también devolverá el token
//             }
//             catch (ValidationException ex)
//             {
//                 return BadRequest(new { error = ex.Message });
//             }
//             catch (EntityNotFoundException)
//             {
//                 return Unauthorized(new { error = "Usuario o contraseña incorrectos" });
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, new { error = "Error interno del servidor", detalle = ex.Message });
//             }
//         }

//     }
// }
