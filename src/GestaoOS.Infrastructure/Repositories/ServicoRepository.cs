using System;
using System.Collections.Generic;
using System.Text;
using GestaoOS.Application.Interfaces;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Filters;
using Npgsql;

namespace GestaoOS.Infrastructure.Repositories
{
    internal class ServicoRepository : RepositoryBase, IServicoRepository
    {
        public ServicoRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
            : base(connection, transaction)
        {
        }

        public Servico ObterPorId(int id)
        {
            using (var command = CreateCommand("SELECT id, nome, valor_base, percentual_imposto, ativo FROM servicos WHERE id = @id"))
            {
                AddParameter(command, "@id", id);
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read() ? Map(reader) : null;
                }
            }
        }

        public IList<Servico> Pesquisar(ServicoFiltro filtro, Paginacao paginacao)
        {
            var sql = new StringBuilder("SELECT id, nome, valor_base, percentual_imposto, ativo FROM servicos WHERE 1 = 1");
            using (var command = CreateCommand(""))
            {
                AplicarFiltros(sql, command, filtro);
                sql.Append(" ORDER BY nome LIMIT @limit OFFSET @offset");
                AddParameter(command, "@limit", paginacao.TamanhoPagina);
                AddParameter(command, "@offset", paginacao.Offset);
                command.CommandText = sql.ToString();

                var servicos = new List<Servico>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        servicos.Add(Map(reader));
                    }
                }

                return servicos;
            }
        }

        public int Contar(ServicoFiltro filtro)
        {
            var sql = new StringBuilder("SELECT COUNT(1) FROM servicos WHERE 1 = 1");
            using (var command = CreateCommand(""))
            {
                AplicarFiltros(sql, command, filtro);
                command.CommandText = sql.ToString();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public int Inserir(Servico servico)
        {
            const string sql = @"INSERT INTO servicos (nome, valor_base, percentual_imposto, ativo)
VALUES (@nome, @valorBase, @percentualImposto, @ativo)
RETURNING id";
            using (var command = CreateCommand(sql))
            {
                AddParameters(command, servico);
                servico.Id = Convert.ToInt32(command.ExecuteScalar());
                return servico.Id;
            }
        }

        public void Atualizar(Servico servico)
        {
            const string sql = @"UPDATE servicos
SET nome = @nome,
    valor_base = @valorBase,
    percentual_imposto = @percentualImposto,
    ativo = @ativo
WHERE id = @id";
            using (var command = CreateCommand(sql))
            {
                AddParameters(command, servico);
                AddParameter(command, "@id", servico.Id);
                command.ExecuteNonQuery();
            }
        }

        private static void AplicarFiltros(StringBuilder sql, NpgsqlCommand command, ServicoFiltro filtro)
        {
            if (filtro == null)
            {
                return;
            }

            if (!string.IsNullOrWhiteSpace(filtro.Nome))
            {
                sql.Append(" AND nome ILIKE @nome");
                AddParameter(command, "@nome", Like(filtro.Nome));
            }

            if (filtro.Ativo.HasValue)
            {
                sql.Append(" AND ativo = @ativo");
                AddParameter(command, "@ativo", filtro.Ativo.Value);
            }
        }

        private static void AddParameters(NpgsqlCommand command, Servico servico)
        {
            AddParameter(command, "@nome", servico.Nome);
            AddParameter(command, "@valorBase", servico.ValorBase);
            AddParameter(command, "@percentualImposto", servico.PercentualImposto);
            AddParameter(command, "@ativo", servico.Ativo);
        }

        private static Servico Map(NpgsqlDataReader reader)
        {
            return new Servico
            {
                Id = Convert.ToInt32(reader["id"]),
                Nome = Convert.ToString(reader["nome"]),
                ValorBase = Convert.ToDecimal(reader["valor_base"]),
                PercentualImposto = Convert.ToDecimal(reader["percentual_imposto"]),
                Ativo = Convert.ToBoolean(reader["ativo"])
            };
        }
    }
}
