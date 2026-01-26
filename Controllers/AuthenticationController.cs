using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Proyecto_Final_ProgramacionWEB.Services.Interfaces;
using Proyecto_Final_ProgramacionWEB.Model.DTOS;
using Proyecto_Final_ProgramacionWEB.Entities;

//TERMINAR ESTO

namespace Proyecto_Final_ProgramacionWEB.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly IConfiguration _config;
        
        public AuthenticationController(IRestaurantService restaurantService, IConfiguration config)
        {
            _restaurantService = restaurantService;
            _config = config;
        }
        [HttpPost]
        public IActionResult Authenticate([FromBody] RestaurantLoginDTO loginDTO)
        {
            // ahora el service recibe el DTO completo y devuelve un DTO de lectura
            var restaurantDto = _restaurantService.Authenticate(loginDTO);

            if (restaurantDto is null)
                return Unauthorized("Email o contraseña incorrectos.");

            var secretKey = _config["Authentication:SecretForKey"]
                            ?? throw new InvalidOperationException("SecretForKey no configurada");

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            var signature = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", restaurantDto.Id_Restaurant.ToString()),
                new Claim("email", restaurantDto.Email)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signature
            );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(tokenToReturn);
        }
    }
}
      
