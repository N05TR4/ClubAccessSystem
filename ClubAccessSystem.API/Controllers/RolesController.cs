using AutoMapper;
using ClubAccessSystem.API.Models.Roles;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Persistence.Exceptions;
using ClubAccessSystem.Persistence.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubAccessSystem.API.Controllers
{
    [Authorize(Roles="1")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepository;
        private readonly IMapper _mapper;

        public RolesController(IRolesRepository rolesRepository, IMapper mapper)
        {
            _rolesRepository = rolesRepository;
            _mapper = mapper;
        }



        [HttpGet("getAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _rolesRepository.GetAll();

                if (roles == null)
                    return NotFound(new { message = "No hay roles disponibles." });

                return Ok(roles);
            }
            catch (RolesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
            }
        }

        [HttpGet("getRolesById/{id}")]
        public async Task<IActionResult> GetRolesById(int id)
        {
            try
            {
                var rol = await _rolesRepository.GetById(id);
                if (rol == null)
                {
                    return NotFound(new { message = $"El rol con el ID {id} no fue encontrado." });
                }
                return Ok(rol);

            }
            catch (RolesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }


        [HttpPost("createRoles")]
        public async Task<IActionResult> CreateRoles([FromBody] AddTipoClientesModels rol)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var roles = _mapper.Map<Roles>(rol);
                await _rolesRepository.Save(roles);

                return Ok(new { messsage = "Operación Exitosa!" });
            }
            catch (RolesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }


        [HttpPut("updateRoles/{id}")]
        public async Task<IActionResult> UpdateRoles(int id, [FromBody] UpdateTipoClientesModels rol)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _rolesRepository.GetById(id);
                if (!result.Success || result.Data == null)
                {
                    return NotFound(new { message = $"El rol con el ID: {id} no fue encontrado." });
                }

                var existingRol = _mapper.Map<Roles>(result.Data);
                _mapper.Map(rol, existingRol);
                existingRol.UpdatedAt = DateTime.Now;

                await _rolesRepository.Update(existingRol);

                return Ok(new { message = "Operación Exitosa!" });
            }
            catch (RolesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

    }
}
