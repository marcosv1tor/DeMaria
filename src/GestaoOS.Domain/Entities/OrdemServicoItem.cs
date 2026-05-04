using System;

namespace GestaoOS.Domain.Entities
{
    public class OrdemServicoItem
    {
        public int Id { get; set; }
        public int OrdemServicoId { get; set; }
        public int ServicoId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal PercentualImpostoAplicado { get; set; }
        public decimal ValorTotalItem { get; set; }

        public void Recalcular()
        {
            ValorTotalItem = CalcularValorTotalItem(Quantidade, ValorUnitario, PercentualImpostoAplicado);
        }

        public static decimal CalcularValorTotalItem(decimal quantidade, decimal valorUnitario, decimal percentualImposto)
        {
            var subtotal = quantidade * valorUnitario;
            var imposto = subtotal * (percentualImposto / 100m);
            return Math.Round(subtotal + imposto, 2, MidpointRounding.AwayFromZero);
        }
    }
}
