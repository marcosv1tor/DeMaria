using System;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;

namespace GestaoOS.WinForms
{
    public static class RelatorioFiltroFactory
    {
        public static OrdemServicoFiltro Criar(DateTime inicio, DateTime fim, bool usarPeriodo, object clienteSelecionado, object statusSelecionado)
        {
            return new OrdemServicoFiltro
            {
                DataInicio = usarPeriodo ? (DateTime?)inicio.Date : null,
                DataFim = usarPeriodo ? (DateTime?)fim.Date : null,
                ClienteId = clienteSelecionado is int ? (int?)clienteSelecionado : null,
                Status = statusSelecionado is StatusOrdemServico ? (StatusOrdemServico?)statusSelecionado : null
            };
        }
    }
}
