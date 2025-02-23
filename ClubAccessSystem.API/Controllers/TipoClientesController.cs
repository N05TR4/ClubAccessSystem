using AutoMapper;
using ClubAccessSystem.API.Models.Roles;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Persistence.Exceptions;
using ClubAccessSystem.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClubAccessSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoClientesController : ControllerBase
    {
        private readonly ITipoClientesRepositorycs _repository;
        private readonly IMapper _mapper;

        public TipoClientesController(ITipoClientesRepositorycs repositorycs, IMapper mapper)
        {
            _repository = repositorycs;
            _mapper = mapper;
        }


        [HttpGet("getAllTiposClientes")]
        public async Task<IActionResult> getAllTiposClientes()
        {
            try
            {
                var tClientes = await _repository.GetAll();

                if (tClientes == null)
                    return NotFound(new { message = "No hay tipos de clientes disponibles." });

                return Ok(tClientes);
            }
            catch (TipoClientesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
            }
        }

        [HttpGet("getTipoClientesById/{id}")]
        public async Task<IActionResult> GetTipoClientesById(int id)
        {
            try
            {
                var tCliente = await _repository.GetById(id);
                if (tCliente == null)
                {
                    return NotFound(new { message = $"El tipo de cliente con el ID {id} no fue encontrado." });
                }
                return Ok(tCliente);

            }
            catch (TipoClientesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }


        [HttpPost("createTipoClientes")]
        public async Task<IActionResult> CreateTipoClientes([FromBody] AddTipoClientesModels models)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var tClientes = _mapper.Map<TipoClientes>(models);
                await _repository.Save(tClientes);

                return Ok(new { messsage = "Operación Exitosa!" });
            }
            catch (TipoClientesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }


        [HttpPut("updateTipoClientes/{id}")]
        public async Task<IActionResult> UpdateTipoClientes(int id, [FromBody] UpdateTipoClientesModels models)
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
                    return NotFound(new { message = $"El tipo de cliente con el ID: {id} no fue encontrado." });
                }

                var existing = _mapper.Map<TipoClientes>(result.Data);
                _mapper.Map(models, existing);
                existing.UpdatedAt = DateTime.Now;

                await _repository.Update(existing);

                return Ok(new { message = "Operación Exitosa!" });
            }
            catch (TipoClientesException ex)
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
