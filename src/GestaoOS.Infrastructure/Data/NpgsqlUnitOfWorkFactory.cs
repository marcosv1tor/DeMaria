using GestaoOS.Application.Interfaces;
using GestaoOS.Infrastructure.Configuration;
using Npgsql;

namespace GestaoOS.Infrastructure.Data
{
    public class NpgsqlUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ConnectionStringProvider _connectionStringProvider;

        public NpgsqlUnitOfWorkFactory()
            : this(new ConnectionStringProvider())
        {
        }

        public NpgsqlUnitOfWorkFactory(ConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public IUnitOfWork Criar()
        {
            var connection = new NpgsqlConnection(_connectionStringProvider.GetConnectionString());
            connection.Open();
            return new NpgsqlUnitOfWork(connection);
        }
    }
}
