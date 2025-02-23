using AutoMapper;
using ClubAccessSystem.API.Models.Clientes;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Persistence.Exceptions;
using ClubAccessSystem.Persistence.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClubAccessSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesRepository _clientesRepository;
        private readonly IMapper _mapper;

        public ClientesController(IClientesRepository clientesRepository, IMapper mapper)
        {
            _clientesRepository = clientesRepository;
            _mapper = mapper;

        }

        [HttpGet("getAllCliente")]
        public async Task<IActionResult> GetAllCliente()
        {
            try
            {
                var cliente = await _clientesRepository.GetAll();

                if (cliente == null)
                    return NotFound(new { message = "No hay clientes disponibles." });

                return Ok(cliente);
            }
            catch (ClientesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor.", details = ex.Message });
            }
        }


        [HttpGet("getClienteById/{id}")]
        public async Task<IActionResult> GetClienteById(int id)
        {
            try
            {
                var cliente = await _clientesRepository.GetById(id);
                if (cliente == null)
                {
                    return NotFound(new { message = $"El cliente con el ID {id} no fue encontrado." });
                }
                return Ok(cliente);

            }
            catch (UsuariosException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Ocurrió un error al procesar la solicitud.", details = ex.Message });
            }
        }


        [HttpPost("createCliente")]
        public async Task<IActionResult> CreateCliente([FromBody] AddClientesModels cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var clientes = _mapper.Map<Clientes>(cliente);
                await _clientesRepository.Save(clientes);

                return Ok(new { messsage = "Operación Exitosa!" });
            }
            catch (ClientesException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { message = "Error interno del servidor", details = ex.Message });
            }
        }

        [HttpPut("updateUsuario/{id}")]
        public async Task<IActionResult> UpdateCliente(int id, [FromBody] UpdateClientesModels cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _clientesRepository.GetById(id);
                if (!result.Success || result.Data == null)
                {
                    return NotFound(new { message = $"El cliente con el ID: {id} no fue encontrado." });
                }

                var existingCliente = _mapper.Map<Clientes>(result.Data);
                _mapper.Map(cliente, existingCliente);
                existingCliente.UpdatedAt = DateTime.Now;

                await _clientesRepository.Update(existingCliente);

                return Ok(new { message = "Operación Exitosa!" });
            }
            catch (UsuariosException ex)
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
