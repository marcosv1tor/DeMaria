using System;
using GestaoOS.Application.Exceptions;
using GestaoOS.Application.Interfaces;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;

namespace GestaoOS.Application.Services
{
    public class OrdemServicoService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public OrdemServicoService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public int Salvar(OrdemServico ordem, string usuario)
        {
            using (var uow = _unitOfWorkFactory.Criar())
            {
                try
                {
                    ValidarParaSalvar(ordem);

                    ordem.RecalcularValorTotal();
                    if (ordem.Status == StatusOrdemServico.Concluida && ordem.ValorTotal <= 0)
                    {
                        throw new RegraNegocioException("Nao e permitido concluir ordem de servico com valor total igual a zero.");
                    }

                    if (ordem.Status == StatusOrdemServico.Concluida && ordem.DataConclusao == null)
                    {
                        ordem.DataConclusao = DateTime.Now;
                    }

                    var operacao = ordem.Id == 0 ? OperacaoAuditoria.INSERT : OperacaoAuditoria.UPDATE;
                    var statusAnterior = ordem.Id == 0 ? (StatusOrdemServico?)null : ObterStatusAnterior(uow, ordem.Id);

                    if (ordem.Id == 0)
                    {
                        ordem.Id = uow.OrdensServico.Inserir(ordem);
                    }
                    else if (!uow.OrdensServico.Atualizar(ordem))
                    {
                        throw new ConcorrenciaException("A ordem de servico foi alterada por outro usuario. Recarregue os dados antes de salvar.");
                    }

                    if (!ordem.ItensBloqueados)
                    {
                        uow.OrdensServico.SubstituirItens(ordem.Id, ordem.Itens);
                    }

                    if (statusAnterior == null || statusAnterior.Value != ordem.Status)
                    {
                        uow.OrdensServico.InserirHistoricoStatus(new HistoricoStatus
                        {
                            OrdemServicoId = ordem.Id,
                            StatusAnterior = statusAnterior,
                            StatusNovo = ordem.Status,
                            Usuario = UsuarioOuPadrao(usuario)
                        });
                    }

                    uow.Auditorias.Registrar(new Auditoria
                    {
                        Entidade = "ordens_servico",
                        IdRegistro = ordem.Id,
                        Operacao = operacao,
                        Usuario = UsuarioOuPadrao(usuario),
                        SnapshotJson = AuditoriaSnapshot.Criar(ordem)
                    });

                    uow.Commit();
                    return ordem.Id;
                }
                catch
                {
                    uow.Rollback();
                    throw;
                }
            }
        }

        public OrdemServico ObterComItens(int id)
        {
            using (var uow = _unitOfWorkFactory.Criar())
            {
                return uow.OrdensServico.ObterComItens(id);
            }
        }

        public ResultadoPaginado<GestaoOS.Domain.DTOs.OrdemServicoResumo> Pesquisar(OrdemServicoFiltro filtro, Paginacao paginacao)
        {
            paginacao = paginacao ?? new Paginacao();
            paginacao.Normalizar();

            using (var uow = _unitOfWorkFactory.Criar())
            {
                return new ResultadoPaginado<GestaoOS.Domain.DTOs.OrdemServicoResumo>(
                    uow.OrdensServico.Pesquisar(filtro ?? new OrdemServicoFiltro(), paginacao),
                    uow.OrdensServico.Contar(filtro ?? new OrdemServicoFiltro()));
            }
        }

        private static void ValidarParaSalvar(OrdemServico ordem)
        {
            if (ordem == null)
            {
                throw new ValidacaoException("Ordem de servico nao informada.");
            }

            if (ordem.ClienteId <= 0)
            {
                throw new ValidacaoException("Cliente e obrigatorio.");
            }

            if (ordem.Id > 0 && ordem.ItensBloqueados && ordem.Itens.Count > 0)
            {
                throw new RegraNegocioException("Nao e permitido editar itens de ordem de servico concluida ou cancelada.");
            }

            foreach (var item in ordem.Itens)
            {
                if (item.ServicoId <= 0)
                {
                    throw new ValidacaoException("Servico do item e obrigatorio.");
                }

                if (item.Quantidade <= 0)
                {
                    throw new ValidacaoException("Quantidade do item deve ser maior que zero.");
                }

                if (item.ValorUnitario <= 0)
                {
                    throw new ValidacaoException("Valor unitario do item deve ser maior que zero.");
                }

                if (item.PercentualImpostoAplicado < 0 || item.PercentualImpostoAplicado > 100)
                {
                    throw new ValidacaoException("Percentual de imposto do item deve estar entre 0 e 100.");
                }
            }
        }

        private static StatusOrdemServico? ObterStatusAnterior(IUnitOfWork uow, int id)
        {
            var existente = uow.OrdensServico.ObterPorId(id);
            return existente == null ? (StatusOrdemServico?)null : existente.Status;
        }

        private static string UsuarioOuPadrao(string usuario)
        {
            return string.IsNullOrWhiteSpace(usuario) ? Environment.UserName : usuario;
        }
    }
}
