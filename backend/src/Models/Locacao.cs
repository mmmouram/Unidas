using System;

namespace MyApp.Models
{
    public class Locacao
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataRetirada { get; set; }
        public DateTime DataDevolucao { get; set; }
        public string ModeloVeiculo { get; set; }
        public decimal ValorPago { get; set; }
        public string LocalRetirada { get; set; }
        public string LocalDevolucao { get; set; }
        public string Status { get; set; }
    }
}
