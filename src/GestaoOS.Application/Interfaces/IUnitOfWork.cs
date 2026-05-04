using System;

namespace GestaoOS.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IClienteRepository Clientes { get; }
        IServicoRepository Servicos { get; }
        IOrdemServicoRepository OrdensServico { get; }
        IAuditoriaRepository Auditorias { get; }
        IRelatorioRepository Relatorios { get; }

        void Commit();
        void Rollback();
    }
}
