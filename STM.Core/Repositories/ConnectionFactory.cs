using Microsoft.Extensions.Configuration;
using Npgsql;
using STM.Core.Helper;
using System.Data;
using System.IO;

namespace STM.Core.Repositories
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string encryptedConnectionString;
        private readonly string connectionString;

        public ConnectionFactory()
        {
            //Simplify this code
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            encryptedConnectionString = configuration["AppSettings:ConnectionString"];
            connectionString = AESEngine.Decrypt(encryptedConnectionString);
        }
        public IDbConnection GetConnection()
        {

            return new NpgsqlConnection(connectionString);

        }
    }
}
