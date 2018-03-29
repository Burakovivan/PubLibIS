using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PubLibIS.DAL
{
    public class DapperConnectionFactory
    {
        private string connectionString;


        public DapperConnectionFactory(string connectionNameOrConnectionString)
        {
            try
            {
                this.connectionString = ConfigurationManager.ConnectionStrings[connectionNameOrConnectionString].ConnectionString;
            }
            catch {
                connectionString = connectionNameOrConnectionString;
            }
        }

        public IDbConnection GetConnectionInstance()
        {
            return new SqlConnection(connectionString);
        }

       
    }
}
