using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DealerApp.Core.DTOs;
using DealerApp.Core.Entities;
using DealerApp.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace DealerApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IMapper _mapper;
        private readonly IHelperImage _helperImage;
        private readonly IPasswordHasher _passwordHasher;
        private readonly string directory;
        public UsersController(ILoginService loginService, IMapper mapper, IHelperImage helperImage, IPasswordHasher passwordHasher, IWebHostEnvironment env)
        {
            _loginService = loginService;
            _mapper = mapper;
            _helperImage = helperImage;
            _passwordHasher = passwordHasher;
            directory = env.ContentRootPath;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromForm] UsuarioDTO usuarioDTO)
        {
            usuarioDTO.Foto = await _helperImage.Upload(usuarioDTO.Image, directory);
            var usuario = _mapper.Map<Usuario>(usuarioDTO);
            usuario.Contrasena = _passwordHasher.Hash(usuario.Contrasena);
            await _loginService.RegisterUser(usuario);
            usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
            return Created(string.Empty, new { usuarioDTO });
        }
    }
}