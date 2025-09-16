using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using MyApp.Controllers;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.Tests.Controllers
{
    [TestFixture]
    public class HistoricoLocacaoControllerTests
    {
        private Mock<IHistoricoLocacaoService> _historicoLocacaoServiceMock;
        private HistoricoLocacaoController _controller;

        [SetUp]
        public void Setup()
        {
            _historicoLocacaoServiceMock = new Mock<IHistoricoLocacaoService>();
            _controller = new HistoricoLocacaoController(_historicoLocacaoServiceMock.Object);
        }

        [Test]
        public async Task ObterHistorico_DeveRetornarBadRequest_QuandoUsuarioIdInvalido()
        {
            // Arrange
            int usuarioId = 0; // inválido

            // Act
            var result = await _controller.ObterHistorico(usuarioId, null, null);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = (BadRequestObjectResult)result;
            Assert.AreEqual("Usuário inválido.", badRequestResult.Value);
        }

        [Test]
        public async Task ObterHistorico_DeveRetornarTodasAsLocacoes_QuandoDatasNaoForemInformadas()
        {
            // Arrange
            int usuarioId = 1;
            var locacoesFake = new List<Locacao>
            {
                new Locacao { Id = 1, UsuarioId = usuarioId, DataRetirada = DateTime.Now.AddDays(-5), DataDevolucao = DateTime.Now.AddDays(-4), ModeloVeiculo = "Modelo X", ValorPago = 100, LocalRetirada = "Local A", LocalDevolucao = "Local B", Status = "Concluída" }
            };
            
            _historicoLocacaoServiceMock.Setup(x => x.ListarHistoricoLocacoesAsync(usuarioId))
                                        .ReturnsAsync(locacoesFake);

            // Act
            var result = await _controller.ObterHistorico(usuarioId, null, null);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(locacoesFake, okResult.Value);
            _historicoLocacaoServiceMock.Verify(x => x.ListarHistoricoLocacoesAsync(usuarioId), Times.Once);
        }

        [Test]
        public async Task ObterHistorico_DeveRetornarLocacoesFiltradas_QuandoDatasForemInformadas()
        {
            // Arrange
            int usuarioId = 1;
            DateTime dataInicio = DateTime.Now.AddDays(-10);
            DateTime dataFim = DateTime.Now.AddDays(-1);
            var locacoesFake = new List<Locacao>
            {
                new Locacao { Id = 2, UsuarioId = usuarioId, DataRetirada = DateTime.Now.AddDays(-8), DataDevolucao = DateTime.Now.AddDays(-7), ModeloVeiculo = "Modelo Y", ValorPago = 150, LocalRetirada = "Local C", LocalDevolucao = "Local D", Status = "Concluída" }
            };
            
            _historicoLocacaoServiceMock.Setup(x => x.FiltrarHistoricoPorPeriodoAsync(usuarioId, dataInicio, dataFim))
                                        .ReturnsAsync(locacoesFake);

            // Act
            var result = await _controller.ObterHistorico(usuarioId, dataInicio, dataFim);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(locacoesFake, okResult.Value);
            _historicoLocacaoServiceMock.Verify(x => x.FiltrarHistoricoPorPeriodoAsync(usuarioId, dataInicio, dataFim), Times.Once);
        }

        [Test]
        public async Task ObterDetalhes_DeveRetornarNotFound_QuandoLocacaoNaoExistir()
        {
            // Arrange
            int locacaoId = 100;
            _historicoLocacaoServiceMock.Setup(x => x.ObterDetalhesLocacaoAsync(locacaoId))
                                        .ReturnsAsync((Locacao)null);

            // Act
            var result = await _controller.ObterDetalhes(locacaoId);

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            var notFoundResult = (NotFoundObjectResult)result;
            Assert.AreEqual("Locação não encontrada.", notFoundResult.Value);
            _historicoLocacaoServiceMock.Verify(x => x.ObterDetalhesLocacaoAsync(locacaoId), Times.Once);
        }

        [Test]
        public async Task ObterDetalhes_DeveRetornarOkComObjetoLocacao_QuandoLocacaoExistir()
        {
            // Arrange
            int locacaoId = 5;
            var locacaoFake = new Locacao
            {
                Id = locacaoId,
                UsuarioId = 1,
                DataRetirada = DateTime.Now.AddDays(-3),
                DataDevolucao = DateTime.Now.AddDays(-2),
                ModeloVeiculo = "Modelo Z",
                ValorPago = 180,
                LocalRetirada = "Local E",
                LocalDevolucao = "Local F",
                Status = "Concluída"
            };
            
            _historicoLocacaoServiceMock.Setup(x => x.ObterDetalhesLocacaoAsync(locacaoId))
                                        .ReturnsAsync(locacaoFake);

            // Act
            var result = await _controller.ObterDetalhes(locacaoId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;
            Assert.AreEqual(locacaoFake, okResult.Value);
            _historicoLocacaoServiceMock.Verify(x => x.ObterDetalhesLocacaoAsync(locacaoId), Times.Once);
        }
    }
}
