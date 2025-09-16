using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using MyApp.Models;
using MyApp.Repositories;
using MyApp.Services;

namespace MyApp.Tests.Services
{
    [TestFixture]
    public class HistoricoLocacaoServiceTests
    {
        private Mock<ILocacaoRepository> _locacaoRepositoryMock;
        private IHistoricoLocacaoService _historicoLocacaoService;

        [SetUp]
        public void Setup()
        {
            _locacaoRepositoryMock = new Mock<ILocacaoRepository>();
            _historicoLocacaoService = new HistoricoLocacaoService(_locacaoRepositoryMock.Object);
        }

        [Test]
        public async Task ListarHistoricoLocacoesAsync_DeveRetornarTodasAsLocacoesDoUsuario()
        {
            // Arrange
            int usuarioId = 1;
            var locacoesFake = new List<Locacao>
            {
                new Locacao { Id = 1, UsuarioId = usuarioId, DataRetirada = DateTime.Now.AddDays(-5), DataDevolucao = DateTime.Now.AddDays(-4), ModeloVeiculo = "Modelo A", ValorPago = 100, LocalRetirada = "Local 1", LocalDevolucao = "Local 2", Status = "Concluída" },
                new Locacao { Id = 2, UsuarioId = usuarioId, DataRetirada = DateTime.Now.AddDays(-3), DataDevolucao = DateTime.Now.AddDays(-2), ModeloVeiculo = "Modelo B", ValorPago = 150, LocalRetirada = "Local 3", LocalDevolucao = "Local 4", Status = "Concluída" }
            };
            
            _locacaoRepositoryMock.Setup(x => x.ObterLocacoesPorUsuarioAsync(usuarioId))
                                  .ReturnsAsync(locacoesFake);

            // Act
            var result = await _historicoLocacaoService.ListarHistoricoLocacoesAsync(usuarioId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, ((List<Locacao>)result).Count);
            _locacaoRepositoryMock.Verify(x => x.ObterLocacoesPorUsuarioAsync(usuarioId), Times.Once);
        }

        [Test]
        public async Task FiltrarHistoricoPorPeriodoAsync_DeveRetornarLocacoesNoIntervaloSelecionado()
        {
            // Arrange
            int usuarioId = 1;
            var dataInicio = DateTime.Now.AddDays(-10);
            var dataFim = DateTime.Now;

            var locacoesFake = new List<Locacao>
            {
                new Locacao { Id = 3, UsuarioId = usuarioId, DataRetirada = DateTime.Now.AddDays(-8), DataDevolucao = DateTime.Now.AddDays(-7), ModeloVeiculo = "Modelo C", ValorPago = 200, LocalRetirada = "Local 5", LocalDevolucao = "Local 6", Status = "Concluída" }
            };
            
            _locacaoRepositoryMock.Setup(x => x.ObterLocacoesPorPeriodoAsync(usuarioId, dataInicio, dataFim))
                                  .ReturnsAsync(locacoesFake);

            // Act
            var result = await _historicoLocacaoService.FiltrarHistoricoPorPeriodoAsync(usuarioId, dataInicio, dataFim);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, ((List<Locacao>)result).Count);
            _locacaoRepositoryMock.Verify(x => x.ObterLocacoesPorPeriodoAsync(usuarioId, dataInicio, dataFim), Times.Once);
        }

        [Test]
        public async Task ObterDetalhesLocacaoAsync_DeveRetornarDetalhesDaLocacaoQuandoExistir()
        {
            // Arrange
            int locacaoId = 10;
            var locacaoFake = new Locacao
            {
                Id = locacaoId,
                UsuarioId = 1,
                DataRetirada = DateTime.Now.AddDays(-5),
                DataDevolucao = DateTime.Now.AddDays(-4),
                ModeloVeiculo = "Modelo D",
                ValorPago = 250,
                LocalRetirada = "Local 7",
                LocalDevolucao = "Local 8",
                Status = "Concluída"
            };
            
            _locacaoRepositoryMock.Setup(x => x.ObterLocacaoPorIdAsync(locacaoId))
                                  .ReturnsAsync(locacaoFake);

            // Act
            var result = await _historicoLocacaoService.ObterDetalhesLocacaoAsync(locacaoId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(locacaoId, result.Id);
            _locacaoRepositoryMock.Verify(x => x.ObterLocacaoPorIdAsync(locacaoId), Times.Once);
        }
    }
}
