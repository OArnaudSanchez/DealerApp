using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DealerApp.Core.Entities;
using DealerApp.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DealerApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILoginService _loginService;
        private readonly IPasswordHasher _passwordHasher;
        public TokenController(IConfiguration configuration, ILoginService loginService, IPasswordHasher passwordHasher)
        {
            _configuration = configuration;
            _loginService = loginService;
            _passwordHasher = passwordHasher;
        }

        [HttpPost]
        public async Task<ActionResult> Authentication([FromForm] UserLogin userLogin)
        {
            var validation = await isValidUser(userLogin);
            if (validation.Item1)
            {
                var token = BuildToken(validation.Item2);
                Response.Headers.Add("X-Token", token);
                return Ok(token);
            }
            return BadRequest();
        }

        private async Task<(bool, Usuario)> isValidUser(UserLogin userLogin)
        {
            var user = await _loginService.GetLoginByCredentials(userLogin);
            var passwordIsValid = _passwordHasher.Check(user.Contrasena, userLogin.Password);

            return (passwordIsValid, user);
        }

        private string BuildToken(Usuario usuario)
        {
            //Header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signInCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signInCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Surname, usuario.Apellidos),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.IdRol.ToString())
            };
            var expirationDate = DateTime.UtcNow.AddMinutes(30);

            //Payload
            var payload = new JwtPayload(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                expirationDate
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}