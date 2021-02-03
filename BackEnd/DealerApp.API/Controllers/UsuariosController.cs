using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DealerApp.API.Responses;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.DTOs;
using DealerApp.Core.Entities;
using DealerApp.Core.Interfaces;
using DealerApp.Core.QueryFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DealerApp.API.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IHelperImage _helperImage;
        private readonly string directory;

        public UsuariosController(IUsuarioService usuarioService, IMapper mapper, IUriService uriService, IHelperImage helperImage)
        {
            _usuarioService = usuarioService;
            _mapper = mapper;
            _uriService = uriService;
            _helperImage = helperImage;
            directory = Directory.GetCurrentDirectory();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> Get([FromQuery] UsuarioQueryFilter filters)
        {
            var location = new ResourceLocation() { Scheme = Request.Scheme, Host = Request.Host.Value, PathBase = Request.PathBase };
            var usuarios = await _usuarioService.GetUsuarios(filters, location);
            var usuariosDTO = _mapper.Map<IEnumerable<UsuarioDTO>>(usuarios);
            var metadata = new MetaData().BuildMeta<Usuario>(usuarios, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<UsuarioDTO>>(usuariosDTO) { Meta = metadata };
            Request.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await DeleteImage(id);
            await _usuarioService.DeleteUsuario(id);
            return NoContent();
        }

        private async Task<bool> DeleteImage(int id)
        {
            var image = await _usuarioService.GetUsuario(id);
            _helperImage.DeleteImage(image.Foto, directory);
            return true;
        }
    }
}