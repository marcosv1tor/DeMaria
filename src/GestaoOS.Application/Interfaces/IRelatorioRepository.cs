using System.Collections.Generic;
using GestaoOS.Domain.DTOs;
using GestaoOS.Domain.Filters;

namespace GestaoOS.Application.Interfaces
{
    public interface IRelatorioRepository
    {
        IList<RelatorioOrdemServico> GerarOrdensServico(OrdemServicoFiltro filtro);
    }
}
