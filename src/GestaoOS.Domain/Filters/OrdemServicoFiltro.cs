using System;
using GestaoOS.Domain.Enums;

namespace GestaoOS.Domain.Filters
{
    public class OrdemServicoFiltro
    {
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int? ClienteId { get; set; }
        public StatusOrdemServico? Status { get; set; }
    }
}
