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
    public class ColoresController : ControllerBase
    {
        private readonly IColorService _colorService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public ColoresController(IColorService colorService, IMapper mapper, IUriService uriService)
        {
            _colorService = colorService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ColorDTO>>> Get([FromQuery] ColorQueryFilter filters)
        {
            var colores = await _colorService.GetColors(filters);
            var coloresDTO = _mapper.Map<IEnumerable<ColorDTO>>(colores);
            var metadata = new MetaData().BuildMeta<Color>(colores, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<ColorDTO>>(coloresDTO) { Meta = metadata };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var color = await _colorService.GetColor(id);
            var colorDTO = _mapper.Map<ColorDTO>(color);
            var response = new ApiResponse<ColorDTO>(colorDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ColorDTO colorDTO)
        {
            var color = _mapper.Map<Color>(colorDTO);
            await _colorService.InsertColor(color);
            colorDTO = _mapper.Map<ColorDTO>(color);
            var response = new ApiResponse<ColorDTO>(colorDTO);
            return Created(nameof(Get), new { id = color.Id, response });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ColorDTO colorDTO)
        {
            var color = _mapper.Map<Color>(colorDTO);
            color.Id = id;
            await _colorService.UpdateColor(color);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _colorService.DeleteColor(id);
            return NoContent();
        }
    }
}