using System.Collections.Generic;
using GestaoOS.Application.Interfaces;
using GestaoOS.Domain.DTOs;
using GestaoOS.Domain.Filters;

namespace GestaoOS.Application.Services
{
    public class RelatorioService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public RelatorioService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public IList<RelatorioOrdemServico> GerarOrdensServico(OrdemServicoFiltro filtro)
        {
            using (var uow = _unitOfWorkFactory.Criar())
            {
                return uow.Relatorios.GerarOrdensServico(filtro ?? new OrdemServicoFiltro());
            }
        }
    }
}
