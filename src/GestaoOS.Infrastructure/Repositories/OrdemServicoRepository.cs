using System;
using System.Collections.Generic;
using System.Text;
using GestaoOS.Application.Interfaces;
using GestaoOS.Domain.DTOs;
using GestaoOS.Domain.Entities;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;
using Npgsql;

namespace GestaoOS.Infrastructure.Repositories
{
    internal class OrdemServicoRepository : RepositoryBase, IOrdemServicoRepository
    {
        public OrdemServicoRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
            : base(connection, transaction)
        {
        }

        public OrdemServico ObterPorId(int id)
        {
            using (var command = CreateCommand(@"SELECT id, cliente_id, data_abertura, data_conclusao, status, observacao, valor_total, versao
FROM ordens_servico WHERE id = @id"))
            {
                AddParameter(command, "@id", id);
                using (var reader = command.ExecuteReader())
                {
                    return reader.Read() ? MapOrdem(reader) : null;
                }
            }
        }

        public OrdemServico ObterComItens(int id)
        {
            var ordem = ObterPorId(id);
            if (ordem == null)
            {
                return null;
            }

            using (var command = CreateCommand(@"SELECT id, ordem_servico_id, servico_id, quantidade, valor_unitario,
percentual_imposto_aplicado, valor_total_item
FROM ordem_servico_itens WHERE ordem_servico_id = @id ORDER BY id"))
            {
                AddParameter(command, "@id", id);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ordem.Itens.Add(MapItem(reader));
                    }
                }
            }

            return ordem;
        }

        public IList<OrdemServicoResumo> Pesquisar(OrdemServicoFiltro filtro, Paginacao paginacao)
        {
            var sql = new StringBuilder(@"SELECT os.id, os.cliente_id, c.nome AS cliente_nome, os.data_abertura,
os.data_conclusao, os.status, os.valor_total, os.versao
FROM ordens_servico os
INNER JOIN clientes c ON c.id = os.cliente_id
WHERE 1 = 1");
            using (var command = CreateCommand(""))
            {
                AplicarFiltros(sql, command, filtro);
                sql.Append(" ORDER BY os.data_abertura DESC, os.id DESC LIMIT @limit OFFSET @offset");
                AddParameter(command, "@limit", paginacao.TamanhoPagina);
                AddParameter(command, "@offset", paginacao.Offset);
                command.CommandText = sql.ToString();

                var ordens = new List<OrdemServicoResumo>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ordens.Add(new OrdemServicoResumo
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            ClienteId = Convert.ToInt32(reader["cliente_id"]),
                            ClienteNome = Convert.ToString(reader["cliente_nome"]),
                            DataAbertura = Convert.ToDateTime(reader["data_abertura"]),
                            DataConclusao = reader["data_conclusao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["data_conclusao"]),
                            Status = (StatusOrdemServico)Enum.Parse(typeof(StatusOrdemServico), Convert.ToString(reader["status"])),
                            ValorTotal = Convert.ToDecimal(reader["valor_total"]),
                            Versao = Convert.ToInt32(reader["versao"])
                        });
                    }
                }

                return ordens;
            }
        }

        public int Contar(OrdemServicoFiltro filtro)
        {
            var sql = new StringBuilder("SELECT COUNT(1) FROM ordens_servico os WHERE 1 = 1");
            using (var command = CreateCommand(""))
            {
                AplicarFiltros(sql, command, filtro);
                command.CommandText = sql.ToString();
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public int Inserir(OrdemServico ordem)
        {
            const string sql = @"INSERT INTO ordens_servico (cliente_id, data_abertura, data_conclusao, status, observacao, valor_total, versao)
VALUES (@clienteId, @dataAbertura, @dataConclusao, @status, @observacao, @valorTotal, 1)
RETURNING id";
            using (var command = CreateCommand(sql))
            {
                AddOrdemParameters(command, ordem);
                ordem.Id = Convert.ToInt32(command.ExecuteScalar());
                ordem.Versao = 1;
                return ordem.Id;
            }
        }

        public bool Atualizar(OrdemServico ordem)
        {
            const string sql = @"UPDATE ordens_servico
SET cliente_id = @clienteId,
    data_abertura = @dataAbertura,
    data_conclusao = @dataConclusao,
    status = @status,
    observacao = @observacao,
    valor_total = @valorTotal,
    versao = versao + 1
WHERE id = @id AND versao = @versao
RETURNING versao";
            using (var command = CreateCommand(sql))
            {
                AddOrdemParameters(command, ordem);
                AddParameter(command, "@id", ordem.Id);
                AddParameter(command, "@versao", ordem.Versao);
                var result = command.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    return false;
                }

                ordem.Versao = Convert.ToInt32(result);
                return true;
            }
        }

        public void SubstituirItens(int ordemServicoId, IList<OrdemServicoItem> itens)
        {
            using (var delete = CreateCommand("DELETE FROM ordem_servico_itens WHERE ordem_servico_id = @ordemServicoId"))
            {
                AddParameter(delete, "@ordemServicoId", ordemServicoId);
                delete.ExecuteNonQuery();
            }

            const string sql = @"INSERT INTO ordem_servico_itens
(ordem_servico_id, servico_id, quantidade, valor_unitario, percentual_imposto_aplicado, valor_total_item)
VALUES (@ordemServicoId, @servicoId, @quantidade, @valorUnitario, @percentualImpostoAplicado, @valorTotalItem)";

            foreach (var item in itens)
            {
                using (var insert = CreateCommand(sql))
                {
                    AddParameter(insert, "@ordemServicoId", ordemServicoId);
                    AddParameter(insert, "@servicoId", item.ServicoId);
                    AddParameter(insert, "@quantidade", item.Quantidade);
                    AddParameter(insert, "@valorUnitario", item.ValorUnitario);
                    AddParameter(insert, "@percentualImpostoAplicado", item.PercentualImpostoAplicado);
                    AddParameter(insert, "@valorTotalItem", item.ValorTotalItem);
                    insert.ExecuteNonQuery();
                }
            }
        }

        public void InserirHistoricoStatus(HistoricoStatus historico)
        {
            const string sql = @"INSERT INTO historico_status (ordem_servico_id, status_anterior, status_novo, data_hora, usuario)
VALUES (@ordemServicoId, @statusAnterior, @statusNovo, @dataHora, @usuario)";
            using (var command = CreateCommand(sql))
            {
                AddParameter(command, "@ordemServicoId", historico.OrdemServicoId);
                AddParameter(command, "@statusAnterior", historico.StatusAnterior.HasValue ? historico.StatusAnterior.Value.ToString() : null);
                AddParameter(command, "@statusNovo", historico.StatusNovo.ToString());
                AddParameter(command, "@dataHora", historico.DataHora);
                AddParameter(command, "@usuario", historico.Usuario);
                command.ExecuteNonQuery();
            }
        }

        private static void AplicarFiltros(StringBuilder sql, NpgsqlCommand command, OrdemServicoFiltro filtro)
        {
            if (filtro == null)
            {
                return;
            }

            if (filtro.DataInicio.HasValue)
            {
                sql.Append(" AND os.data_abertura >= @dataInicio");
                AddParameter(command, "@dataInicio", filtro.DataInicio.Value.Date);
            }

            if (filtro.DataFim.HasValue)
            {
                sql.Append(" AND os.data_abertura < @dataFim");
                AddParameter(command, "@dataFim", filtro.DataFim.Value.Date.AddDays(1));
            }

            if (filtro.ClienteId.HasValue)
            {
                sql.Append(" AND os.cliente_id = @clienteId");
                AddParameter(command, "@clienteId", filtro.ClienteId.Value);
            }

            if (filtro.Status.HasValue)
            {
                sql.Append(" AND os.status = @status");
                AddParameter(command, "@status", filtro.Status.Value.ToString());
            }
        }

        private static void AddOrdemParameters(NpgsqlCommand command, OrdemServico ordem)
        {
            AddParameter(command, "@clienteId", ordem.ClienteId);
            AddParameter(command, "@dataAbertura", ordem.DataAbertura);
            AddParameter(command, "@dataConclusao", ordem.DataConclusao);
            AddParameter(command, "@status", ordem.Status.ToString());
            AddParameter(command, "@observacao", ordem.Observacao);
            AddParameter(command, "@valorTotal", ordem.ValorTotal);
        }

        private static OrdemServico MapOrdem(NpgsqlDataReader reader)
        {
            return new OrdemServico
            {
                Id = Convert.ToInt32(reader["id"]),
                ClienteId = Convert.ToInt32(reader["cliente_id"]),
                DataAbertura = Convert.ToDateTime(reader["data_abertura"]),
                DataConclusao = reader["data_conclusao"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["data_conclusao"]),
                Status = (StatusOrdemServico)Enum.Parse(typeof(StatusOrdemServico), Convert.ToString(reader["status"])),
                Observacao = reader["observacao"] == DBNull.Value ? null : Convert.ToString(reader["observacao"]),
                ValorTotal = Convert.ToDecimal(reader["valor_total"]),
                Versao = Convert.ToInt32(reader["versao"])
            };
        }

        private static OrdemServicoItem MapItem(NpgsqlDataReader reader)
        {
            return new OrdemServicoItem
            {
                Id = Convert.ToInt32(reader["id"]),
                OrdemServicoId = Convert.ToInt32(reader["ordem_servico_id"]),
                ServicoId = Convert.ToInt32(reader["servico_id"]),
                Quantidade = Convert.ToDecimal(reader["quantidade"]),
                ValorUnitario = Convert.ToDecimal(reader["valor_unitario"]),
                PercentualImpostoAplicado = Convert.ToDecimal(reader["percentual_imposto_aplicado"]),
                ValorTotalItem = Convert.ToDecimal(reader["valor_total_item"])
            };
        }
    }
}
