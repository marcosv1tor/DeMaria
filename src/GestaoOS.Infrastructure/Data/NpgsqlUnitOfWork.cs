using System;
using GestaoOS.Application.Interfaces;
using GestaoOS.Infrastructure.Repositories;
using Npgsql;

namespace GestaoOS.Infrastructure.Data
{
    internal sealed class NpgsqlUnitOfWork : IUnitOfWork
    {
        private readonly NpgsqlConnection _connection;
        private readonly NpgsqlTransaction _transaction;
        private bool _completed;

        public NpgsqlUnitOfWork(NpgsqlConnection connection)
        {
            _connection = connection;
            _transaction = connection.BeginTransaction();
            Clientes = new ClienteRepository(connection, _transaction);
            Servicos = new ServicoRepository(connection, _transaction);
            OrdensServico = new OrdemServicoRepository(connection, _transaction);
            Auditorias = new AuditoriaRepository(connection, _transaction);
            Relatorios = new RelatorioRepository(connection, _transaction);
        }

        public IClienteRepository Clientes { get; private set; }
        public IServicoRepository Servicos { get; private set; }
        public IOrdemServicoRepository OrdensServico { get; private set; }
        public IAuditoriaRepository Auditorias { get; private set; }
        public IRelatorioRepository Relatorios { get; private set; }

        public void Commit()
        {
            _transaction.Commit();
            _completed = true;
        }

        public void Rollback()
        {
            if (!_completed)
            {
                _transaction.Rollback();
                _completed = true;
            }
        }

        public void Dispose()
        {
            if (!_completed)
            {
                try
                {
                    _transaction.Rollback();
                }
                catch
                {
                }
            }

            _transaction.Dispose();
            _connection.Dispose();
        }
    }
}
