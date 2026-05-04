using System;
using System.Collections.Generic;
using System.Text;
using GestaoOS.Application.Interfaces;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;
using Npgsql;

namespace GestaoOS.Infrastructure.Repositories
{
    internal class ClienteRepository : RepositoryBase, IClienteRepository
    {
        public ClienteRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
            : base(connection, transaction)
        {
        }

        public Cliente ObterPorId(int id)
        {
            using (var command = CreateCommand("SELECT id, nome, documento, tipo, email, telefone, data_cadastro, ativo FROM clientes WHERE id = @id"))
            {
                AddParameter(command, "@id", id);
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read() ? Map(reader) : null;
                }
            }
        }

        public bool ExisteDocumento(string documento, int idIgnorado)
        {
            using (var command = CreateCommand("SELECT COUNT(1) FROM clientes WHERE documento = @documento AND id <> @idIgnorado"))
            {
                AddParameter(command, "@documento", documento);
                AddParameter(command, "@idIgnorado", idIgnorado);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public bool ExisteOrdemServicoVinculada(int clienteId)
        {
            using (var command = CreateCommand("SELECT COUNT(1) FROM ordens_servico WHERE cliente_id = @clienteId"))
            {
                AddParameter(command, "@clienteId", clienteId);
                return Convert.ToInt32(command.ExecuteScalar()) > 0;
            }
        }

        public IList<Cliente> Pesquisar(ClienteFiltro filtro, Paginacao paginacao)
        {
            var sql = new StringBuilder("SELECT id, nome, documento, tipo, email, telefone, data_cadastro, ativo FROM clientes WHERE 1 = 1");
            using (var command = CreateCommand(""))
            {
                AplicarFiltros(sql, command, filtro);
                sql.Append(" ORDER BY nome LIMIT @limit OFFSET @offset");
                AddParameter(command, "@limit", paginacao.TamanhoPagina);
                AddParameter(command, "@offset", paginacao.Offset);
                command.CommandText = sql.ToString();

                var clientes = new List<Cliente>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clientes.Add(Map(reader));
                    }
                }

                return clientes;
            }
        }

        public int Contar(ClienteFiltro filtro)
        {
            var sql = new StringBuilder("SELECT COUNT(1) FROM clientes WHERE 1 = 1");
            using (var command = CreateCommand(""))
            {
                AplicarFiltros(sql, command, filtro);
                command.CommandText = sql.ToString();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public int Inserir(Cliente cliente)
        {
            const string sql = @"INSERT INTO clientes (nome, documento, tipo, email, telefone, data_cadastro, ativo)
VALUES (@nome, @documento, @tipo, @email, @telefone, @dataCadastro, @ativo)
RETURNING id";
            using (var command = CreateCommand(sql))
            {
                AddParameters(command, cliente);
                cliente.Id = Convert.ToInt32(command.ExecuteScalar());
                return cliente.Id;
            }
        }

        public void Atualizar(Cliente cliente)
        {
            const string sql = @"UPDATE clientes
SET nome = @nome,
    documento = @documento,
    tipo = @tipo,
    email = @email,
    telefone = @telefone,
    ativo = @ativo
WHERE id = @id";
            using (var command = CreateCommand(sql))
            {
                AddParameters(command, cliente);
                AddParameter(command, "@id", cliente.Id);
                command.ExecuteNonQuery();
            }
        }

        public void Excluir(int id)
        {
            using (var command = CreateCommand("DELETE FROM clientes WHERE id = @id"))
            {
                AddParameter(command, "@id", id);
                command.ExecuteNonQuery();
            }
        }

        private static void AplicarFiltros(StringBuilder sql, NpgsqlCommand command, ClienteFiltro filtro)
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

            if (!string.IsNullOrWhiteSpace(filtro.Documento))
            {
                sql.Append(" AND documento ILIKE @documento");
                AddParameter(command, "@documento", Like(filtro.Documento));
            }

            if (filtro.Ativo.HasValue)
            {
                sql.Append(" AND ativo = @ativo");
                AddParameter(command, "@ativo", filtro.Ativo.Value);
            }
        }

        private static void AddParameters(NpgsqlCommand command, Cliente cliente)
        {
            AddParameter(command, "@nome", cliente.Nome);
            AddParameter(command, "@documento", cliente.Documento);
            AddParameter(command, "@tipo", cliente.Tipo.ToString());
            AddParameter(command, "@email", cliente.Email);
            AddParameter(command, "@telefone", cliente.Telefone);
            AddParameter(command, "@dataCadastro", cliente.DataCadastro);
            AddParameter(command, "@ativo", cliente.Ativo);
        }

        private static Cliente Map(NpgsqlDataReader reader)
        {
            return new Cliente
            {
                Id = Convert.ToInt32(reader["id"]),
                Nome = Convert.ToString(reader["nome"]),
                Documento = Convert.ToString(reader["documento"]),
                Tipo = (TipoCliente)Enum.Parse(typeof(TipoCliente), Convert.ToString(reader["tipo"])),
                Email = reader["email"] == DBNull.Value ? null : Convert.ToString(reader["email"]),
                Telefone = reader["telefone"] == DBNull.Value ? null : Convert.ToString(reader["telefone"]),
                DataCadastro = Convert.ToDateTime(reader["data_cadastro"]),
                Ativo = Convert.ToBoolean(reader["ativo"])
            };
        }
    }
}
