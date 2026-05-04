using System;
using System.Data;
using Npgsql;

namespace GestaoOS.Infrastructure.Repositories
{
    internal abstract class RepositoryBase
    {
        private readonly NpgsqlConnection _connection;
        private readonly NpgsqlTransaction _transaction;

        protected RepositoryBase(NpgsqlConnection connection, NpgsqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        protected NpgsqlCommand CreateCommand(string sql)
        {
            var command = _connection.CreateCommand();
            command.Transaction = _transaction;
            command.CommandText = sql;
            return command;
        }

        protected static void AddParameter(NpgsqlCommand command, string name, object value)
        {
            command.Parameters.AddWithValue(name, value ?? DBNull.Value);
        }

        protected static T ReadEnum<T>(IDataRecord record, string name)
        {
            return (T)Enum.Parse(typeof(T), Convert.ToString(record[name]));
        }

        protected static string Like(string value)
        {
            return "%" + value.Trim() + "%";
        }
    }
}
