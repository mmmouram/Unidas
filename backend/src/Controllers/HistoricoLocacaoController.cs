using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using MyApp.Services;

namespace MyApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoLocacaoController : ControllerBase
    {
        private readonly IHistoricoLocacaoService _historicoLocacaoService;

        public HistoricoLocacaoController(IHistoricoLocacaoService historicoLocacaoService)
        {
            _historicoLocacaoService = historicoLocacaoService;
        }

        // GET: api/historicolocacao?usuarioId={usuarioId}&dataInicio={dataInicio}&dataFim={dataFim}
        [HttpGet]
        public async Task<IActionResult> ObterHistorico([FromQuery] int usuarioId, [FromQuery] DateTime? dataInicio, [FromQuery] DateTime? dataFim)
        {
            if (usuarioId <= 0)
            {
                return BadRequest("Usuário inválido.");
            }

            IEnumerable<Locacao> locacoes;

            if (dataInicio.HasValue && dataFim.HasValue)
            {
                locacoes = await _historicoLocacaoService.FiltrarHistoricoPorPeriodoAsync(usuarioId, dataInicio.Value, dataFim.Value);
            }
            else
            {
                locacoes = await _historicoLocacaoService.ListarHistoricoLocacoesAsync(usuarioId);
            }

            return Ok(locacoes);
        }

        // GET: api/historicolocacao/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> ObterDetalhes(int id)
        {
            var locacao = await _historicoLocacaoService.ObterDetalhesLocacaoAsync(id);
            if (locacao == null)
            {
                return NotFound("Locação não encontrada.");
            }
            return Ok(locacao);
        }
    }
}
