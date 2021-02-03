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
using Microsoft.AspNetCore.Hosting;

namespace DealerApp.API.Controllers
{
    [Authorize(Roles = "Administrador, Empleado")]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        private readonly IHelperImage _helperImage;
        private readonly string directory;
        private readonly string folder;
        public ClientesController(IClienteService clienteService, IMapper mapper, IUriService uriService,
        IHelperImage helperImage, IWebHostEnvironment env)
        {
            _clienteService = clienteService;
            _mapper = mapper;
            _uriService = uriService;
            _helperImage = helperImage;
            directory = env.ContentRootPath;
            folder = this.GetType().Name.Replace("Controller", "");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get([FromQuery] ClienteQueryFilter filters)
        {
            var location = new ResourceLocation() { Scheme = Request.Scheme, Host = Request.Host.Value, PathBase = Request.PathBase };
            var clientes = await _clienteService.GetClientes(filters, location);
            var clientesDTO = _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
            var metadata = new MetaData().BuildMeta<Cliente>(clientes, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<ClienteDTO>>(clientesDTO) { Meta = metadata };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var cliente = await _clienteService.GetCliente(id);
            var clienteDTo = _mapper.Map<ClienteDTO>(cliente);
            var response = new ApiResponse<ClienteDTO>(clienteDTo);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ClienteDTO clienteDTO)
        {
            clienteDTO.Foto = await _helperImage.Upload(clienteDTO.Image, directory, folder);
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            await _clienteService.InsertCliente(cliente);
            clienteDTO = _mapper.Map<ClienteDTO>(cliente);
            var response = new ApiResponse<ClienteDTO>(clienteDTO);
            return Created(nameof(Get), new { id = cliente.Id, response });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ClienteDTO clienteDTO)
        {
            await DeleteImage(id);
            clienteDTO.Foto = await _helperImage.Upload(clienteDTO.Image, directory, folder);
            var cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Id = id;
            await _clienteService.UpdateCliente(cliente);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await DeleteImage(id);
            await _clienteService.DeleteCliente(id);
            return NoContent();
        }

        private async Task<bool> DeleteImage(int id)
        {
            var image = await _clienteService.GetCliente(id);
            _helperImage.DeleteImage(image.Foto, folder, directory);
            return true;
        }
    }
}