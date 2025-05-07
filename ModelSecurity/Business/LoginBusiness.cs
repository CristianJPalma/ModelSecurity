using Data;
using Entity.DTOs;
using Entity.Model;
using Microsoft.Extensions.Logging;
using Utilities.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Business
{   
    public class LoginBusiness
    {
        private readonly UserData _userData;
        private readonly ILogger<LoginBusiness> _logger;

        public LoginBusiness(UserData userData, ILogger<LoginBusiness> logger)
        {
            _userData = userData;
            _logger = logger;
        }

        public async Task<object> LoginAsync(LoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
                throw new ValidationException("Login", "Email y contraseña son obligatorios");

            var user = await _userData.GetUserWithPersonByEmailAsync(loginDto.Email);
            if (user == null)
                throw new EntityNotFoundException("Usuario", loginDto.Email);

            // Validar la contraseña (aún sin hashing)
            if (loginDto.Password != user.Password)
                throw new ValidationException("Password", "Contraseña incorrecta");

            if (string.IsNullOrEmpty(user.Password))
                throw new ValidationException("Password", "El usuario no tiene contraseña registrada");

            if (loginDto.Password != user.Password)
                throw new ValidationException("Password", "Contraseña incorrecta");

            // Generar JWT
            var token = GenerateJwtToken(user);

            return new
            {
                Message = "Login exitoso",
                User = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName
                },
                Token = token
            };
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("EsteEsUnSecretoSuperSeguroDe32Caracteres!!"); // Usa una clave segura
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Person.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(1), // Establece la duración del token
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        


    }
}