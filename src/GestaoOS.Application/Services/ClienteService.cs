using GestaoOS.Application.Exceptions;
using GestaoOS.Application.Interfaces;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Filters;

namespace GestaoOS.Application.Services
{
    public class ClienteService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ClienteService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public static void Validar(Cliente cliente)
        {
            if (cliente == null)
            {
                throw new ValidacaoException("Cliente nao informado.");
            }

            if (string.IsNullOrWhiteSpace(cliente.Nome))
            {
                throw new ValidacaoException("Nome do cliente e obrigatorio.");
            }

            if (string.IsNullOrWhiteSpace(cliente.Documento))
            {
                throw new ValidacaoException("Documento do cliente e obrigatorio.");
            }
        }

        public int Salvar(Cliente cliente)
        {
            Validar(cliente);

            using (var uow = _unitOfWorkFactory.Criar())
            {
                try
                {
                    if (uow.Clientes.ExisteDocumento(cliente.Documento, cliente.Id))
                    {
                        throw new ValidacaoException("Ja existe cliente cadastrado com este documento.");
                    }

                    var id = cliente.Id == 0 ? uow.Clientes.Inserir(cliente) : cliente.Id;
                    if (cliente.Id > 0)
                    {
                        uow.Clientes.Atualizar(cliente);
                    }

                    uow.Commit();
                    return id;
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }
        }

        public void Excluir(int id)
        {
            using (var uow = _unitOfWorkFactory.Criar())
            {
                try
                {
                    if (uow.Clientes.ExisteOrdemServicoVinculada(id))
                    {
                        throw new RegraNegocioException("Cliente possui ordem de servico vinculada e nao pode ser excluido.");
                    }

                    uow.Clientes.Excluir(id);
                    uow.Commit();
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }
        }

        public ResultadoPaginado<Cliente> Pesquisar(ClienteFiltro filtro, Paginacao paginacao)
        {
            paginacao = paginacao ?? new Paginacao();
            paginacao.Normalizar();

            using (var uow = _unitOfWorkFactory.Criar())
            {
                return new ResultadoPaginado<Cliente>(
                    uow.Clientes.Pesquisar(filtro ?? new ClienteFiltro(), paginacao),
                    uow.Clientes.Contar(filtro ?? new ClienteFiltro()));
            }
        }
    }
}
