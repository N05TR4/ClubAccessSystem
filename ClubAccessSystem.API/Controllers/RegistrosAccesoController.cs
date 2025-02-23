
using AutoMapper;
using ClubAccessSystem.API.Models.RegistrosAcceso;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Persistence.Exceptions;
using ClubAccessSystem.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClubAccessSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrosAccesoController : ControllerBase
    {
        private readonly IRegistrosAccesoRepository _repository;
        private readonly IMapper _mapper;

        public RegistrosAccesoController(IRegistrosAccesoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }



        [HttpGet("getAllRegistrosAcceso")]
        public async Task<IActionResult> GetAllRegistrosAcceso()
        {
            try
            {
                var rAcceso = await _repository.GetAll();

                if (rAcceso == null)
                    return NotFound(new { message = "No hay registros de accesos disponibles." });

                return Ok(rAcceso);
            }
            catch (RegistrosAccesoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
            }
        }

        [HttpGet("getRegistrosAccesoById/{id}")]
        public async Task<IActionResult> GetRegistrosAccesoById(int id)
        {
            try
            {
                var rAcceso = await _repository.GetById(id);
                if (rAcceso == null)
                {
                    return NotFound(new { message = $"El registro de acceso con el ID {id} no fue encontrado." });
                }
                return Ok(rAcceso);

            }
            catch (RegistrosAccesoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }


        [HttpPost("createRegistrosAcceso")]
        public async Task<IActionResult> CreateRegistrosAcceso([FromBody] AddRegistrosAccesoModels models)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var rAcceso = _mapper.Map<RegistrosAcceso>(models);
                await _repository.Save(rAcceso);

                return Ok(new { messsage = "Operación Exitosa!" });
            }
            catch (RegistrosAccesoException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }


        [HttpPut("updateRegistrosAcceso/{id}")]
        public async Task<IActionResult> UpdateRegistrosAcceso(int id, [FromBody] UpdateRegistrosAccesoModels models)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _repository.GetById(id);
                if (!result.Success || result.Data == null)
                {
                    return NotFound(new { message = $"El registro de acceso con el ID: {id} no fue encontrado." });
                }

                var existing = _mapper.Map<RegistrosAcceso>(result.Data);
                _mapper.Map(models, existing);
                existing.UpdatedAt = DateTime.Now;

                await _repository.Update(existing);

                return Ok(new { message = "Operación Exitosa!" });
            }
            catch (RegistrosAccesoException ex)
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
