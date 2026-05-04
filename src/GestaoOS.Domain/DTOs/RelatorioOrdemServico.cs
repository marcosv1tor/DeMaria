using System;
using GestaoOS.Domain.Enums;

namespace GestaoOS.Domain.DTOs
{
    public class RelatorioOrdemServico
    {
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public int OrdemServicoId { get; set; }
        public DateTime DataAbertura { get; set; }
        public StatusOrdemServico Status { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal TotalImpostos { get; set; }
        public int QuantidadeOrdens { get; set; }
    }
}
