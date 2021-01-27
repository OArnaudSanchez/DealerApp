using System.Collections.Generic;
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
    [Authorize(Roles = "Administrador, Empleado")]
    [ApiController]
    [Route("api/[controller]")]
    public class ContratosController : ControllerBase
    {
        private readonly IContratoService _contratoService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public ContratosController(IContratoService contratoService, IMapper mapper, IUriService uriService)
        {
            _contratoService = contratoService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContratoDTO>>> Get([FromQuery] ContratoQueryFilter filters)
        {
            var contratos = await _contratoService.GetContratos(filters);
            var contratosDTO = _mapper.Map<IEnumerable<ContratoDTO>>(contratos);
            var metadata = new MetaData().BuildMeta<Contrato>(contratos, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<ContratoDTO>>(contratosDTO) { Meta = metadata };
            Request.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var contrato = await _contratoService.GetContrato(id);
            var contratoDTO = _mapper.Map<ContratoDTO>(contrato);
            var response = new ApiResponse<ContratoDTO>(contratoDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ContratoDTO contratoDTO)
        {
            var contrato = _mapper.Map<Contrato>(contratoDTO);
            await _contratoService.InsertContrato(contrato);
            contratoDTO = _mapper.Map<ContratoDTO>(contrato);
            var response = new ApiResponse<ContratoDTO>(contratoDTO);
            return Created(nameof(Get), new { id = contrato.Id, response });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ContratoDTO contratoDTO)
        {
            var contrato = _mapper.Map<Contrato>(contratoDTO);
            contrato.Id = id;
            await _contratoService.UpdateContrato(contrato);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _contratoService.DeleteContrato(id);
            return NoContent();
        }
    }
}