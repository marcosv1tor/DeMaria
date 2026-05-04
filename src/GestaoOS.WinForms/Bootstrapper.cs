using GestaoOS.Application.Services;
using GestaoOS.Infrastructure.Data;
using GestaoOS.Infrastructure.Logging;

namespace GestaoOS.WinForms
{
    internal static class Bootstrapper
    {
        private static readonly NpgsqlUnitOfWorkFactory UnitOfWorkFactory = new NpgsqlUnitOfWorkFactory();

        public static readonly FileAppLogger Logger = new FileAppLogger();

        public static ClienteService ClienteService()
        {
            return new ClienteService(UnitOfWorkFactory);
        }

        public static ServicoService ServicoService()
        {
            return new ServicoService(UnitOfWorkFactory);
        }

        public static OrdemServicoService OrdemServicoService()
        {
            return new OrdemServicoService(UnitOfWorkFactory);
        }

        public static RelatorioService RelatorioService()
        {
            return new RelatorioService(UnitOfWorkFactory);
        }
    }
}
