using GestaoOS.Application.Exceptions;
using GestaoOS.Application.Interfaces;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Filters;

namespace GestaoOS.Application.Services
{
    public class ServicoService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ServicoService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public static void Validar(Servico servico)
        {
            if (servico == null)
            {
                throw new ValidacaoException("Servico nao informado.");
            }

            if (string.IsNullOrWhiteSpace(servico.Nome))
            {
                throw new ValidacaoException("Nome do servico e obrigatorio.");
            }

            if (servico.ValorBase <= 0)
            {
                throw new ValidacaoException("Valor base deve ser maior que zero.");
            }

            if (servico.PercentualImposto < 0 || servico.PercentualImposto > 100)
            {
                throw new ValidacaoException("Percentual de imposto deve estar entre 0 e 100.");
            }
        }

        public int Salvar(Servico servico)
        {
            Validar(servico);

            using (var uow = _unitOfWorkFactory.Criar())
            {
                try
                {
                    var id = servico.Id == 0 ? uow.Servicos.Inserir(servico) : servico.Id;
                    if (servico.Id > 0)
                    {
                        uow.Servicos.Atualizar(servico);
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

        public ResultadoPaginado<Servico> Pesquisar(ServicoFiltro filtro, Paginacao paginacao)
        {
            paginacao = paginacao ?? new Paginacao();
            paginacao.Normalizar();

            using (var uow = _unitOfWorkFactory.Criar())
            {
                return new ResultadoPaginado<Servico>(
                    uow.Servicos.Pesquisar(filtro ?? new ServicoFiltro(), paginacao),
                    uow.Servicos.Contar(filtro ?? new ServicoFiltro()));
            }
        }
    }
}
