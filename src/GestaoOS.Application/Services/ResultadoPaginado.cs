using System.Collections.Generic;

namespace GestaoOS.Application.Services
{
    public class ResultadoPaginado<T>
    {
        public ResultadoPaginado(IList<T> itens, int total)
        {
            Itens = itens;
            Total = total;
        }

        public IList<T> Itens { get; private set; }
        public int Total { get; private set; }
    }
}
