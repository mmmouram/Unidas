using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.Repositories
{
    public interface ILocacaoRepository
    {
        Task<IEnumerable<Locacao>> ObterLocacoesPorUsuarioAsync(int usuarioId);
        Task<Locacao> ObterLocacaoPorIdAsync(int id);
        Task<IEnumerable<Locacao>> ObterLocacoesPorPeriodoAsync(int usuarioId, DateTime dataInicio, DateTime dataFim);
    }
}
