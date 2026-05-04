using System.Configuration;

namespace GestaoOS.Infrastructure.Configuration
{
    public class ConnectionStringProvider
    {
        private readonly string _name;

        public ConnectionStringProvider()
            : this("GestaoOS")
        {
        }

        public ConnectionStringProvider(string name)
        {
            _name = name;
        }

        public string GetConnectionString()
        {
            var configured = ConfigurationManager.ConnectionStrings[_name];
            if (configured != null && !string.IsNullOrWhiteSpace(configured.ConnectionString))
            {
                return configured.ConnectionString;
            }

            return "Host=localhost;Port=5432;Database=gestao_os;Username=postgres;Password=postgres";
        }
    }
}
