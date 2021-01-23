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
    public class CombustiblesController : ControllerBase
    {
        private readonly ICombustibleService _combustibleService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public CombustiblesController(ICombustibleService combustibleService, IMapper mapper, IUriService uriService)
        {
            _combustibleService = combustibleService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CombustibleDTO>>> Get([FromQuery]CombustibleQueryFilter filters)
        {
            var combustibles = await _combustibleService.GetCombustibles(filters);
            var combustiblesDTO = _mapper.Map<IEnumerable<CombustibleDTO>>(combustibles);
            var metadata = new MetaData().BuildMeta<Combustible>(combustibles, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<CombustibleDTO>>(combustiblesDTO) { Meta = metadata };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var combustible = await _combustibleService.GetCombustible(id);
            var combustibleDTO = _mapper.Map<CombustibleDTO>(combustible);
            var response = new ApiResponse<CombustibleDTO>(combustibleDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] CombustibleDTO combustibleDTO)
        {
            var combustible = _mapper.Map<Combustible>(combustibleDTO);
            await _combustibleService.InsertCombustible(combustible);
            combustibleDTO = _mapper.Map<CombustibleDTO>(combustible);
            var response = new ApiResponse<CombustibleDTO>(combustibleDTO);
            return Created(nameof(Get), new {id = combustible.Id, response});
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] CombustibleDTO combustibleDTO)
        {
            var updateCombustible = _mapper.Map<Combustible>(combustibleDTO);
            updateCombustible.Id = id;
            await _combustibleService.UpdateCombustible(updateCombustible);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {   
            await _combustibleService.DeleteCombustible(id);
            return NoContent();
        }
    }
}