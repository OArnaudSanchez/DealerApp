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

namespace DealerApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SangreClientesController : ControllerBase
    {
        private readonly ISangreClienteService _sangreClienteService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public SangreClientesController(ISangreClienteService sangreClienteService, IMapper mapper, IUriService uriService)
        {
            _mapper = mapper;
            _sangreClienteService = sangreClienteService;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SangreClienteDTO>>> Get([FromQuery] SangreClienteQueryFilter filters)
        {
            var sangres = await _sangreClienteService.GetSangreClientes(filters);
            var sangresDTO = _mapper.Map<IEnumerable<SangreClienteDTO>>(sangres);
            var metadata = new MetaData().BuildMeta<SangreCliente>(sangres, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<SangreClienteDTO>>(sangresDTO) { Meta = metadata };
            Request.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var sangre = await _sangreClienteService.GetSangreCliente(id);
            var sangreDTO = _mapper.Map<SangreClienteDTO>(sangre);
            var response = new ApiResponse<SangreClienteDTO>(sangreDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] SangreClienteDTO sangreClienteDTO)
        {
            var sangre = _mapper.Map<SangreCliente>(sangreClienteDTO);
            await _sangreClienteService.InsertSangreCliente(sangre);
            sangreClienteDTO = _mapper.Map<SangreClienteDTO>(sangre);
            var response = new ApiResponse<SangreClienteDTO>(sangreClienteDTO);
            return Created(nameof(Get), new { id = sangre.Id, response });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] SangreClienteDTO sangreClienteDTO)
        {
            var sangre = _mapper.Map<SangreCliente>(sangreClienteDTO);
            sangre.Id = id;
            await _sangreClienteService.UpdateSangreCliente(sangre);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _sangreClienteService.DeleteSangreCliente(id);
            return NoContent();
        }
    }
}