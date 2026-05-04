namespace GestaoOS.Domain.Filters
{
    public class Paginacao
    {
        public Paginacao()
        {
            Pagina = 1;
            TamanhoPagina = 20;
        }

        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }

        public int Offset
        {
            get { return (Pagina - 1) * TamanhoPagina; }
        }

        public void Normalizar()
        {
            if (Pagina < 1)
            {
                Pagina = 1;
            }

            if (TamanhoPagina < 1)
            {
                TamanhoPagina = 20;
            }

            if (TamanhoPagina > 200)
            {
                TamanhoPagina = 200;
            }
        }
    }
}
