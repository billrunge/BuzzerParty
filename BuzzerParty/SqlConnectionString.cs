using System;
using System.Threading.Tasks;

namespace BuzzerParty
{
    public class SqlConnectionString
    {
        public string Server { get; } = Environment.GetEnvironmentVariable("SQL_SERVER");
        public int Port { get; }
        public string DatabaseName { get; } = Environment.GetEnvironmentVariable("SQL_DATABASE_NAME");
        public string UserName { get; } = Environment.GetEnvironmentVariable("SQL_USER_NAME");
        public string Password { get; } = Environment.GetEnvironmentVariable("SQL_PASSWORD");
        public int Timeout { get; }

        public SqlConnectionString()
        {
           if (int.TryParse(Environment.GetEnvironmentVariable("SQL_TIMEOUT"), out int timeout))
            {
                Timeout = timeout;
            }
            else
            {
                Timeout = 30;
            }

            if (int.TryParse(Environment.GetEnvironmentVariable("SQL_PORT"), out int port))
            {
                Port = port;
            }
            else
            {
                Port = 1433;
            }
        }

        public string GetSqlConnectionString()
        {
            return $"Server={Server},{Port};" +
                $"Initial Catalog={DatabaseName};" +
                $"User ID={UserName};" +
                $"Password={Password};" +
                $"Connection Timeout={Timeout};";
        }
    }
}
