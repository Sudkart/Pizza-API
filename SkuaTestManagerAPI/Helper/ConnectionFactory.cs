using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.IO;
using STM.Core.Repositories;

namespace STMAPI.Helper
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly string encryptedConnectionString;
        private readonly string connectionString;
        private readonly string APIConnectionString;

        public ConnectionFactory()
        {
            //Simplify this code
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            encryptedConnectionString = configuration["AppSettings:ConnectionString"];
            connectionString =STMAPI.Helper.AESEngine.Decrypt(encryptedConnectionString);
            APIConnectionString = configuration["AppSettings:APIQAURL"];
        }
        public IDbConnection GetConnection()
        {

            return new NpgsqlConnection(connectionString);

        }
    }
}
