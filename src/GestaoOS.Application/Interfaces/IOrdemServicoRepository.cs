using System.Collections.Generic;
using GestaoOS.Domain.DTOs;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Filters;

namespace GestaoOS.Application.Interfaces
{
    public interface IOrdemServicoRepository
    {
        OrdemServico ObterPorId(int id);
        OrdemServico ObterComItens(int id);
        IList<OrdemServicoResumo> Pesquisar(OrdemServicoFiltro filtro, Paginacao paginacao);
        int Contar(OrdemServicoFiltro filtro);
        int Inserir(OrdemServico ordem);
        bool Atualizar(OrdemServico ordem);
        void SubstituirItens(int ordemServicoId, IList<OrdemServicoItem> itens);
        void InserirHistoricoStatus(HistoricoStatus historico);
    }
}
