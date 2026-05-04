using GestaoOS.Domain.Entities;
using Newtonsoft.Json;

namespace GestaoOS.Application.Services
{
    internal static class AuditoriaSnapshot
    {
        public static string Criar(OrdemServico ordem)
        {
            return JsonConvert.SerializeObject(new
            {
                ordem.Id,
                ordem.ClienteId,
                ordem.DataAbertura,
                ordem.DataConclusao,
                Status = ordem.Status.ToString(),
                ordem.Observacao,
                ordem.ValorTotal,
                ordem.Versao,
                Itens = ordem.Itens
            });
        }
    }
}
