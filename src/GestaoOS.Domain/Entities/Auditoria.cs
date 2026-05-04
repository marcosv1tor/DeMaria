using System;
using GestaoOS.Domain.Enums;

namespace GestaoOS.Domain.Entities
{
    public class Auditoria
    {
        public Auditoria()
        {
            DataHora = DateTime.Now;
        }

        public int Id { get; set; }
        public string Entidade { get; set; }
        public int IdRegistro { get; set; }
        public OperacaoAuditoria Operacao { get; set; }
        public DateTime DataHora { get; set; }
        public string Usuario { get; set; }
        public string SnapshotJson { get; set; }
    }
}
