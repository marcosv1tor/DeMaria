using System.Collections.Generic;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Filters;

namespace GestaoOS.Application.Interfaces
{
    public interface IClienteRepository
    {
        Cliente ObterPorId(int id);
        bool ExisteDocumento(string documento, int idIgnorado);
        bool ExisteOrdemServicoVinculada(int clienteId);
        IList<Cliente> Pesquisar(ClienteFiltro filtro, Paginacao paginacao);
        int Contar(ClienteFiltro filtro);
        int Inserir(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(int id);
    }
}
