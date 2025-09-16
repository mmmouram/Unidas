using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.Services
{
    public interface IHistoricoLocacaoService
    {
        Task<IEnumerable<Locacao>> ListarHistoricoLocacoesAsync(int usuarioId);
        Task<IEnumerable<Locacao>> FiltrarHistoricoPorPeriodoAsync(int usuarioId, DateTime dataInicio, DateTime dataFim);
        Task<Locacao> ObterDetalhesLocacaoAsync(int locacaoId);
    }
}
