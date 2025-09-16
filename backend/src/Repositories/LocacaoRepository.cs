using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyApp.Models;
using MyApp.Data;

namespace MyApp.Repositories
{
    public class LocacaoRepository : ILocacaoRepository
    {
        private readonly AppDbContext _context;

        public LocacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Locacao>> ObterLocacoesPorUsuarioAsync(int usuarioId)
        {
            return await _context.Locacoes
                                 .Where(l => l.UsuarioId == usuarioId)
                                 .OrderBy(l => l.DataRetirada)
                                 .ToListAsync();
        }

        public async Task<Locacao> ObterLocacaoPorIdAsync(int id)
        {
            return await _context.Locacoes.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Locacao>> ObterLocacoesPorPeriodoAsync(int usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            return await _context.Locacoes
                                 .Where(l => l.UsuarioId == usuarioId &&
                                             l.DataRetirada >= dataInicio &&
                                             l.DataRetirada <= dataFim)
                                 .OrderBy(l => l.DataRetirada)
                                 .ToListAsync();
        }
    }
}
