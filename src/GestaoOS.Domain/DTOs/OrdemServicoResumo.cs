using System;
using GestaoOS.Domain.Enums;

namespace GestaoOS.Domain.DTOs
{
    public class OrdemServicoResumo
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public string ClienteNome { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public StatusOrdemServico Status { get; set; }
        public decimal ValorTotal { get; set; }
        public int Versao { get; set; }
    }
}
