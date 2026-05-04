using GestaoOS.Application.Interfaces;
using GestaoOS.Domain.Entities;
using Npgsql;

namespace GestaoOS.Infrastructure.Repositories
{
    internal class AuditoriaRepository : RepositoryBase, IAuditoriaRepository
    {
        public AuditoriaRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
            : base(connection, transaction)
        {
        }

        public void Registrar(Auditoria auditoria)
        {
            const string sql = @"INSERT INTO auditorias (entidade, id_registro, operacao, data_hora, usuario, snapshot_json)
VALUES (@entidade, @idRegistro, @operacao, @dataHora, @usuario, CAST(@snapshot AS jsonb))";
            using (var command = CreateCommand(sql))
            {
                AddParameter(command, "@entidade", auditoria.Entidade);
                AddParameter(command, "@idRegistro", auditoria.IdRegistro);
                AddParameter(command, "@operacao", auditoria.Operacao.ToString());
                AddParameter(command, "@dataHora", auditoria.DataHora);
                AddParameter(command, "@usuario", auditoria.Usuario);
                AddParameter(command, "@snapshot", auditoria.SnapshotJson);
                command.ExecuteNonQuery();
            }
        }
    }
}
