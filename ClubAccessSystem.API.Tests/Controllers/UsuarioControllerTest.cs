
using Xunit;
using Moq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ClubAccessSystem.API.Controllers;
using ClubAccessSystem.Domain.Entities;
using ClubAccessSystem.Persistence.Context;
using ClubAccessSystem.Persistence.Interfaces;
using ClubAccessSystem.API.Models.Usuarios;
using ClubAccessSystem.Persistence.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClubAccessSystem.Domain.Result;

namespace ClubAccessSystem.API.Tests.Controllers
{
    public class UsuarioControllerTest
    {
        private readonly Mock<IUsuariosRepository> _mockRepository;
        private readonly Mock<DbSet<Usuarios>> _mockDbSet;
        private readonly ClubContext _context;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<UsuarioController>> _mockLogger;
        private readonly UsuarioController _controller;

        public UsuarioControllerTest()
        {
            // Configurar el mock del DbSet
            _mockDbSet = new Mock<DbSet<Usuarios>>();

            // Crear una instancia real del DbContext con opciones en memoria
            var options = new DbContextOptionsBuilder<ClubContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
            _context = new ClubContext(options);

            // Configurar los demás mocks
            _mockRepository = new Mock<IUsuariosRepository>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<UsuarioController>>();

            // Inicializar el controlador
            _controller = new UsuarioController(
                _mockRepository.Object,
                _context,
                _mockMapper.Object,
                _mockLogger.Object
            );
        }


        [Fact]
        public async Task GetAllUsuario_WhenUsersExist_ReturnsOkResult()
        {
            // Arrange
            var usuarios = new List<Usuarios> { new Usuarios { UsuarioId = 1, Nombre = "Test" } };
            var usuariosModels = new List<UsuariosRolModels> { new UsuariosRolModels { UsuarioId = 1, Nombre = "Test" } };

            _mockMapper.Setup(m => m.Map<List<UsuariosRolModels>>(usuarios))
                .Returns(usuariosModels);

            _mockRepository.Setup(repo => repo.GetAllUsuarioRoll())
                .Returns(Task.FromResult(new OperationResult<List<UsuariosRolModels>>
                {
                    Success = true,
                    Data = usuariosModels
                }));

            // Act
            var result = await _controller.GetAllUsuario();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<OperationResult<List<UsuariosRolModels>>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.NotNull(returnValue.Data);
        }


        [Fact]
        public async Task GetUsuarioById_WhenUserExists_ReturnsOkResult()
        {
            // Arrange
            var id = 1;
            var usuario = new Usuarios { UsuarioId = 1, Nombre = "Test" };
            var usuarioModel = new UsuariosModels { UsuarioId = 1, Nombre = "Test" };

            _mockRepository.Setup(repo => repo.GetById(id))
                .Returns(Task.FromResult(new OperationResult<UsuariosModels>
                {
                    Success = true,
                    Data = usuarioModel
                }));

            // Act
            var result = await _controller.GetUsuarioById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<OperationResult<UsuariosModels>>(okResult.Value);
            Assert.True(returnValue.Success);
            Assert.NotNull(returnValue.Data);
        }

        [Fact]
        public async Task CreateUsuario_WithValidModel_ReturnsOkResult()
        {
            // Arrange
            var usuarioModel = new AddUsuariosModels { Nombre = "Test" };
            var usuario = new Usuarios { UsuarioId = 1, Nombre = "Test" };
            var savedUsuarioModel = new UsuariosModels { UsuarioId = 1, Nombre = "Test" };

            _mockMapper.Setup(m => m.Map<Usuarios>(usuarioModel))
                .Returns(usuario);

            _mockRepository.Setup(repo => repo.Save(It.IsAny<Usuarios>()))
                .Returns(Task.FromResult(new OperationResult<UsuariosModels>
                {
                    Success = true,
                    Data = savedUsuarioModel
                }));

            // Act
            var result = await _controller.CreateUsuario(usuarioModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }


        [Fact]
        public async Task DeleteUsuario_WhenUserDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var id = 1;

            _mockDbSet.Setup(d => d.FindAsync(id))
                .Returns(new ValueTask<Usuarios>((Usuarios)null));

            // Act
            var result = await _controller.DeleteUsuario(id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
