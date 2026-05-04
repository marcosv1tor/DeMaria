using System.Collections.Generic;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Filters;

namespace GestaoOS.Application.Interfaces
{
    public interface IServicoRepository
    {
        Servico ObterPorId(int id);
        IList<Servico> Pesquisar(ServicoFiltro filtro, Paginacao paginacao);
        int Contar(ServicoFiltro filtro);
        int Inserir(Servico servico);
        void Atualizar(Servico servico);
    }
}
