using System;
using System.Collections.Generic;
using System.Linq;
using GestaoOS.Domain.Enums;

namespace GestaoOS.Domain.Entities
{
    public class OrdemServico
    {
        public OrdemServico()
        {
            DataAbertura = DateTime.Now;
            Status = StatusOrdemServico.Aberta;
            Versao = 1;
            Itens = new List<OrdemServicoItem>();
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime? DataConclusao { get; set; }
        public StatusOrdemServico Status { get; set; }
        public string Observacao { get; set; }
        public decimal ValorTotal { get; set; }
        public int Versao { get; set; }
        public IList<OrdemServicoItem> Itens { get; private set; }

        public bool ItensBloqueados
        {
            get { return Status == StatusOrdemServico.Concluida || Status == StatusOrdemServico.Cancelada; }
        }

        public void RecalcularValorTotal()
        {
            foreach (var item in Itens)
            {
                item.Recalcular();
            }

            ValorTotal = Math.Round(Itens.Sum(item => item.ValorTotalItem), 2, MidpointRounding.AwayFromZero);
        }
    }
}
