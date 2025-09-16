using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Models;
using MyApp.Repositories;

namespace MyApp.Services
{
    public class HistoricoLocacaoService : IHistoricoLocacaoService
    {
        private readonly ILocacaoRepository _locacaoRepository;

        public HistoricoLocacaoService(ILocacaoRepository locacaoRepository)
        {
            _locacaoRepository = locacaoRepository;
        }

        public async Task<IEnumerable<Locacao>> ListarHistoricoLocacoesAsync(int usuarioId)
        {
            return await _locacaoRepository.ObterLocacoesPorUsuarioAsync(usuarioId);
        }

        public async Task<IEnumerable<Locacao>> FiltrarHistoricoPorPeriodoAsync(int usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            return await _locacaoRepository.ObterLocacoesPorPeriodoAsync(usuarioId, dataInicio, dataFim);
        }

        public async Task<Locacao> ObterDetalhesLocacaoAsync(int locacaoId)
        {
            return await _locacaoRepository.ObterLocacaoPorIdAsync(locacaoId);
        }
    }
}
