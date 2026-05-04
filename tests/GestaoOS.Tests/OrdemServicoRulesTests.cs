using System;
using System.Collections.Generic;
using GestaoOS.Application.Exceptions;
using GestaoOS.Application.Interfaces;
using GestaoOS.Application.Services;
using GestaoOS.Domain.DTOs;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestaoOS.Tests
{
    [TestClass]
    public class OrdemServicoRulesTests
    {
        [TestMethod]
        public void OrdemServicoItem_Recalcular_AplicaQuantidadeEImposto()
        {
            var item = new OrdemServicoItem
            {
                Quantidade = 2,
                ValorUnitario = 100,
                PercentualImpostoAplicado = 10
            };

            item.Recalcular();

            Assert.AreEqual(220m, item.ValorTotalItem);
        }

        [TestMethod]
        public void OrdemServico_RecalcularValorTotal_SomaItens()
        {
            var ordem = new OrdemServico();
            ordem.Itens.Add(new OrdemServicoItem { Quantidade = 1, ValorUnitario = 100, PercentualImpostoAplicado = 10 });
            ordem.Itens.Add(new OrdemServicoItem { Quantidade = 2, ValorUnitario = 50, PercentualImpostoAplicado = 5 });

            ordem.RecalcularValorTotal();

            Assert.AreEqual(215m, ordem.ValorTotal);
        }

        [TestMethod]
        public void OrdemServicoService_Salvar_BloqueiaItensQuandoConcluida()
        {
            var uow = new FakeUnitOfWork();
            var service = new OrdemServicoService(new FakeUnitOfWorkFactory(uow));
            var ordem = CriarOrdemValida();
            ordem.Id = 10;
            ordem.Status = StatusOrdemServico.Concluida;

            Assert.ThrowsException<RegraNegocioException>(() => service.Salvar(ordem, "tester"));
            Assert.IsTrue(uow.RollbackChamado);
            Assert.IsFalse(uow.CommitChamado);
        }

        [TestMethod]
        public void OrdemServicoService_Salvar_NaoConcluiComValorZero()
        {
            var uow = new FakeUnitOfWork();
            var service = new OrdemServicoService(new FakeUnitOfWorkFactory(uow));
            var ordem = new OrdemServico
            {
                ClienteId = 1,
                Status = StatusOrdemServico.Concluida,
                DataConclusao = DateTime.Today
            };

            Assert.ThrowsException<RegraNegocioException>(() => service.Salvar(ordem, "tester"));
            Assert.IsTrue(uow.RollbackChamado);
            Assert.IsFalse(uow.CommitChamado);
        }

        [TestMethod]
        public void OrdemServicoService_Salvar_LancaConcorrenciaQuandoUpdateNaoAfetaLinha()
        {
            var uow = new FakeUnitOfWork();
            uow.OrdemServicoRepository.AtualizacaoConcorrente = true;
            var service = new OrdemServicoService(new FakeUnitOfWorkFactory(uow));
            var ordem = CriarOrdemValida();
            ordem.Id = 7;
            ordem.Versao = 3;

            Assert.ThrowsException<ConcorrenciaException>(() => service.Salvar(ordem, "tester"));
            Assert.IsTrue(uow.RollbackChamado);
            Assert.IsFalse(uow.CommitChamado);
        }

        [TestMethod]
        public void OrdemServicoService_Salvar_RollbackQuandoItensFalham()
        {
            var uow = new FakeUnitOfWork();
            uow.OrdemServicoRepository.FalharAoSubstituirItens = true;
            var service = new OrdemServicoService(new FakeUnitOfWorkFactory(uow));
            var ordem = CriarOrdemValida();

            Assert.ThrowsException<InvalidOperationException>(() => service.Salvar(ordem, "tester"));
            Assert.IsTrue(uow.RollbackChamado);
            Assert.IsFalse(uow.CommitChamado);
            Assert.AreEqual(1, uow.OrdemServicoRepository.CabecalhosInseridos);
        }

        private static OrdemServico CriarOrdemValida()
        {
            var ordem = new OrdemServico
            {
                ClienteId = 1,
                Status = StatusOrdemServico.Aberta,
                Observacao = "Teste"
            };
            ordem.Itens.Add(new OrdemServicoItem
            {
                ServicoId = 1,
                Quantidade = 1,
                ValorUnitario = 100,
                PercentualImpostoAplicado = 10
            });
            return ordem;
        }
    }

    internal sealed class FakeUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly FakeUnitOfWork _unitOfWork;

        public FakeUnitOfWorkFactory(FakeUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IUnitOfWork Criar()
        {
            return _unitOfWork;
        }
    }

    internal sealed class FakeUnitOfWork : IUnitOfWork
    {
        public FakeUnitOfWork()
        {
            OrdemServicoRepository = new FakeOrdemServicoRepository();
            AuditoriaRepository = new FakeAuditoriaRepository();
        }

        public IClienteRepository Clientes { get; set; }
        public IServicoRepository Servicos { get; set; }
        public IOrdemServicoRepository OrdensServico { get { return OrdemServicoRepository; } }
        public IAuditoriaRepository Auditorias { get { return AuditoriaRepository; } }
        public IRelatorioRepository Relatorios { get; set; }
        public FakeOrdemServicoRepository OrdemServicoRepository { get; private set; }
        public FakeAuditoriaRepository AuditoriaRepository { get; private set; }
        public bool CommitChamado { get; private set; }
        public bool RollbackChamado { get; private set; }

        public void Commit()
        {
            CommitChamado = true;
        }

        public void Rollback()
        {
            RollbackChamado = true;
        }

        public void Dispose()
        {
        }
    }

    internal sealed class FakeOrdemServicoRepository : IOrdemServicoRepository
    {
        public bool AtualizacaoConcorrente { get; set; }
        public bool FalharAoSubstituirItens { get; set; }
        public int CabecalhosInseridos { get; private set; }

        public OrdemServico ObterPorId(int id)
        {
            return null;
        }

        public OrdemServico ObterComItens(int id)
        {
            return null;
        }

        public IList<OrdemServicoResumo> Pesquisar(OrdemServicoFiltro filtro, Paginacao paginacao)
        {
            return new List<OrdemServicoResumo>();
        }

        public int Contar(OrdemServicoFiltro filtro)
        {
            return 0;
        }

        public int Inserir(OrdemServico ordem)
        {
            CabecalhosInseridos++;
            ordem.Id = 99;
            ordem.Versao = 1;
            return ordem.Id;
        }

        public bool Atualizar(OrdemServico ordem)
        {
            return !AtualizacaoConcorrente;
        }

        public void SubstituirItens(int ordemServicoId, IList<OrdemServicoItem> itens)
        {
            if (FalharAoSubstituirItens)
            {
                throw new InvalidOperationException("Falha simulada");
            }
        }

        public void InserirHistoricoStatus(HistoricoStatus historico)
        {
        }
    }

    internal sealed class FakeAuditoriaRepository : IAuditoriaRepository
    {
        public IList<Auditoria> Auditorias { get; private set; }

        public FakeAuditoriaRepository()
        {
            Auditorias = new List<Auditoria>();
        }

        public void Registrar(Auditoria auditoria)
        {
            Auditorias.Add(auditoria);
        }
    }
}
