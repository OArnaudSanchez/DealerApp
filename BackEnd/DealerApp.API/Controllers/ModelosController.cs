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
    public class ModelosController : ControllerBase
    {
        private readonly IModeloService _modeloService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public ModelosController(IModeloService modeloService, IMapper mapper, IUriService uriService)
        {
            _modeloService = modeloService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModeloDTO>>> Get([FromQuery] ModeloQueryFilter filters)
        {
            var modelos = await _modeloService.GetModelos(filters);
            var modelosDTO = _mapper.Map<IEnumerable<ModeloDTO>>(modelos);
            var metadata = new MetaData().BuildMeta<Modelo>(modelos, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<ModeloDTO>>(modelosDTO) { Meta = metadata };
            Request.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var modelo = await _modeloService.GetModelo(id);
            var modeloDTO = _mapper.Map<ModeloDTO>(modelo);
            var response = new ApiResponse<ModeloDTO>(modeloDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ModeloDTO modeloDTO)
        {
            var modelo = _mapper.Map<Modelo>(modeloDTO);
            await _modeloService.InsertModelo(modelo);
            modeloDTO = _mapper.Map<ModeloDTO>(modelo);
            var response = new ApiResponse<ModeloDTO>(modeloDTO);
            return Created(nameof(Get), new { id = modelo.Id, response });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ModeloDTO modeloDTO)
        {
            var modelo = _mapper.Map<Modelo>(modeloDTO);
            modelo.Id = id;
            await _modeloService.UpdateModelo(modelo);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _modeloService.DeleteModelo(id);
            return NoContent();
        }
    }
}