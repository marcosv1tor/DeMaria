using System;

namespace GestaoOS.WinForms
{
    public static class OrdemServicoDateTime
    {
        public static DateTime ResolverDataAbertura(DateTime dataSelecionada, DateTime momentoSalvar, bool novaOrdem)
        {
            return novaOrdem
                ? dataSelecionada.Date.Add(momentoSalvar.TimeOfDay)
                : dataSelecionada;
        }
    }
}
