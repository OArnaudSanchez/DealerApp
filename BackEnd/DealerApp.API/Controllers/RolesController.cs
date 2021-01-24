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
    public class RolesController : ControllerBase
    {
        private readonly IRolService _rolService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;
        public RolesController(IRolService rolService, IMapper mapper, IUriService uriService)
        {
            _rolService = rolService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] RolQueryFilter filters)
        {
            var roles = await _rolService.GetRoles(filters);
            var rolesDTO = _mapper.Map<IEnumerable<RolDTO>>(roles);
            var metadata = new MetaData().BuildMeta<Rol>(roles, filters, Request.Path.Value, _uriService);
            var response = new ApiResponse<IEnumerable<RolDTO>>(rolesDTO) { Meta = metadata };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var rol = await _rolService.GetRol(id);
            var rolDTO = _mapper.Map<RolDTO>(rol);
            var response = new ApiResponse<RolDTO>(rolDTO);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] RolDTO rolDTO)
        {
            var rol = _mapper.Map<Rol>(rolDTO);
            await _rolService.InsertRol(rol);
            rolDTO = _mapper.Map<RolDTO>(rol);
            var response = new ApiResponse<RolDTO>(rolDTO);
            return Created(nameof(Get), new { id = rol.Id, response });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] RolDTO rolDTO)
        {
            var rol = _mapper.Map<Rol>(rolDTO);
            rol.Id = id;
            await _rolService.UpdateRol(rol);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _rolService.DeleteRol(id);
            return NoContent();
        }
    }
}