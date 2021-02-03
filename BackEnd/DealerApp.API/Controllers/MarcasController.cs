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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DealerApp.API.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IHelperImage _helperImage;
        private readonly string directory;
        private readonly string folder;
        public MarcasController(IMarcaService marcaService, IMapper mapper, IUriService uriService, IHelperImage helperImage, IWebHostEnvironment env)
        {
            _marcaService = marcaService;
            _mapper = mapper;
            _uriService = uriService;
            _helperImage = helperImage;
            directory = env.ContentRootPath;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaDTO>>> Get([FromQuery] MarcaQueryFilter filters)
        {
            var location = new ResourceLocation() { Scheme = Request.Scheme, Host = Request.Host.Value, PathBase = Request.PathBase };
            var marcas = await _marcaService.GetMarcas(filters, location);
            var marcasDTO = _mapper.Map<IEnumerable<MarcaDTO>>(marcas);
            var metadata = new MetaData().BuildMeta<Marca>(marcas, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<MarcaDTO>>(marcasDTO) { Meta = metadata };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var marca = await _marcaService.GetMarca(id);
            var marcaDTO = _mapper.Map<MarcaDTO>(marca);
            var response = new ApiResponse<MarcaDTO>(marcaDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] MarcaDTO marcaDTO)
        {
            marcaDTO.Foto = await _helperImage.Upload(marcaDTO.Image, directory);
            var marca = _mapper.Map<Marca>(marcaDTO);
            await _marcaService.InsertMarca(marca);
            marcaDTO = _mapper.Map<MarcaDTO>(marca);
            var response = new ApiResponse<MarcaDTO>(marcaDTO);
            return Created(nameof(Get), new { id = marca.Id, response });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] MarcaDTO marcaDTO)
        {
            await DeleteImage(id);
            marcaDTO.Foto = await _helperImage.Upload(marcaDTO.Image, directory);
            var marca = _mapper.Map<Marca>(marcaDTO);
            marca.Id = id;
            await _marcaService.UpdateMarca(marca);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await DeleteImage(id);
            await _marcaService.DeleteMarca(id);
            return NoContent();
        }

        private async Task<bool> DeleteImage(int id)
        {
            var image = await _marcaService.GetMarca(id);
            _helperImage.DeleteImage(image.Foto, directory);
            return true;
        }
    }
}