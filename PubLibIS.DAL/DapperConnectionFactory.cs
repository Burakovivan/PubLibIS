using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PubLibIS.DAL
{
    public class DapperConnectionFactory
    {
        private string connectionString;

        public string Schema { get; private set; }

        public DapperConnectionFactory(string connectionNameOrConnectionString, string schema = "dbo")
        {
            try
            {
                this.connectionString = ConfigurationManager.ConnectionStrings[connectionNameOrConnectionString].ConnectionString;
            }
            catch {
                connectionString = connectionNameOrConnectionString;
            }
            this.Schema = schema;
        }
        public IDbConnection GetConnectionInstance()
        {
            return new SqlConnection(connectionString);
        }

       
    }
}
