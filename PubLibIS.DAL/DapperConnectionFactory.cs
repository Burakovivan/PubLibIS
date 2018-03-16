using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PubLibIS.DAL
{
    public class DapperConnectionFactory
    {
        private string connectionString;

        public string Schema { get; private set; }

        public DapperConnectionFactory(string connectionName, string schema = "dbo")
        {
            this.connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString; ;
            this.Schema = schema;
        }
        public IDbConnection GetConnectionInstance()
        {
            return new SqlConnection(connectionString);
        }

       
    }
}
