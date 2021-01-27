using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DealerApp.API.Responses;
using DealerApp.Core.CustomEntities;
using DealerApp.Core.DTOs;
using DealerApp.Core.Entities;
using DealerApp.Core.Interfaces;
using DealerApp.Core.QueryFilters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace DealerApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculosController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IHelperImage _helperImage;
        private readonly string directory;
        private readonly string folder;
        public VehiculosController(IVehiculoService vehiculoService, IMapper mapper, IUriService uriService, IHelperImage helperImage)
        {
            _helperImage = helperImage;
            _mapper = mapper;
            _uriService = uriService;
            _vehiculoService = vehiculoService;
            directory = Directory.GetCurrentDirectory();
            folder = this.GetType().Name.Replace("Controller", "");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehiculoDTO>>> Get([FromQuery] VehiculoQueryFilter filters)
        {
            var location = new ResourceLocation() { Scheme = Request.Scheme, Host = Request.Host.Value, PathBase = Request.PathBase };
            var vehiculos = await _vehiculoService.GetVehiculos(filters, location);
            var vehiculoDTO = _mapper.Map<IEnumerable<VehiculoDTO>>(vehiculos);
            var metadata = new MetaData().BuildMeta<Vehiculo>(vehiculos, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<VehiculoDTO>>(vehiculoDTO) { Meta = metadata };
            Request.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var vehiculo = await _vehiculoService.GetVehiculo(id);
            var vehiculoDTO = _mapper.Map<VehiculoDTO>(vehiculo);
            var response = new ApiResponse<VehiculoDTO>(vehiculoDTO);
            return Ok(response);
        }

        [Authorize(Roles = "Administrador, Empleado")]
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] VehiculoDTO vehiculoDTO)
        {
            vehiculoDTO.Foto = await _helperImage.Upload(vehiculoDTO.Image, directory, folder);
            var vehiculo = _mapper.Map<Vehiculo>(vehiculoDTO);
            await _vehiculoService.InsertVehiculo(vehiculo);
            vehiculoDTO = _mapper.Map<VehiculoDTO>(vehiculo);
            var response = new ApiResponse<VehiculoDTO>(vehiculoDTO);
            return Created(nameof(Get), new { id = vehiculo.Id, response });
        }

        [Authorize(Roles = "Administrador, Empleado")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] VehiculoDTO vehiculoDTO)
        {
            await DeleteImage(id);
            vehiculoDTO.Foto = await _helperImage.Upload(vehiculoDTO.Image, directory, folder);
            var vehiculo = _mapper.Map<Vehiculo>(vehiculoDTO);
            vehiculo.Id = id;
            await _vehiculoService.UpdateVehiculo(vehiculo);
            return NoContent();
        }

        [Authorize(Roles = "Administrador, Empleado")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await DeleteImage(id);
            await _vehiculoService.DeleteVehiculo(id);
            return NoContent();
        }

        private async Task<bool> DeleteImage(int id)
        {
            var image = await _vehiculoService.GetVehiculo(id);
            _helperImage.DeleteImage(image.Foto, folder, directory);
            return true;
        }
    }
}