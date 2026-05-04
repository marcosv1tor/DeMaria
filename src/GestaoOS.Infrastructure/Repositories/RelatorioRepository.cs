using System;
using System.Collections.Generic;
using System.Text;
using GestaoOS.Application.Interfaces;
using GestaoOS.Domain.DTOs;
using GestaoOS.Domain.Enums;
using GestaoOS.Domain.Filters;
using Npgsql;

namespace GestaoOS.Infrastructure.Repositories
{
    internal class RelatorioRepository : RepositoryBase, IRelatorioRepository
    {
        public RelatorioRepository(NpgsqlConnection connection, NpgsqlTransaction transaction)
            : base(connection, transaction)
        {
        }

        public IList<RelatorioOrdemServico> GerarOrdensServico(OrdemServicoFiltro filtro)
        {
            var sql = new StringBuilder(@"SELECT
    c.id AS cliente_id,
    c.nome AS cliente_nome,
    os.id AS ordem_servico_id,
    os.data_abertura,
    os.status,
    os.valor_total,
    COALESCE(SUM((i.quantidade * i.valor_unitario) * (i.percentual_imposto_aplicado / 100)), 0) AS total_impostos,
    COUNT(os.id) OVER (PARTITION BY c.id) AS quantidade_ordens
FROM ordens_servico os
INNER JOIN clientes c ON c.id = os.cliente_id
LEFT JOIN ordem_servico_itens i ON i.ordem_servico_id = os.id
WHERE 1 = 1");

            using (var command = CreateCommand(""))
            {
                AplicarFiltros(sql, command, filtro);
                sql.Append(@" GROUP BY c.id, c.nome, os.id, os.data_abertura, os.status, os.valor_total
ORDER BY c.nome, os.data_abertura, os.id");
                command.CommandText = sql.ToString();

                var rows = new List<RelatorioOrdemServico>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        rows.Add(new RelatorioOrdemServico
                        {
                            ClienteId = Convert.ToInt32(reader["cliente_id"]),
                            ClienteNome = Convert.ToString(reader["cliente_nome"]),
                            OrdemServicoId = Convert.ToInt32(reader["ordem_servico_id"]),
                            DataAbertura = Convert.ToDateTime(reader["data_abertura"]),
                            Status = (StatusOrdemServico)Enum.Parse(typeof(StatusOrdemServico), Convert.ToString(reader["status"])),
                            ValorTotal = Convert.ToDecimal(reader["valor_total"]),
                            TotalImpostos = Convert.ToDecimal(reader["total_impostos"]),
                            QuantidadeOrdens = Convert.ToInt32(reader["quantidade_ordens"])
                        });
                    }
                }

                return rows;
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
    }
}
