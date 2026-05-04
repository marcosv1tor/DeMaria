using System;
using System.Globalization;

namespace GestaoOS.WinForms
{
    public static class OrdemServicoGridPresentation
    {
        public const string ConclusaoNaoInformada = "--";

        public static string FormatarConclusao(DateTime? dataConclusao)
        {
            return dataConclusao.HasValue
                ? dataConclusao.Value.ToString("d", CultureInfo.CurrentCulture)
                : ConclusaoNaoInformada;
        }
    }
}
