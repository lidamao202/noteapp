using Microsoft.Data.SqlClient;
using System.Data;

namespace noteapi
{
    public class DapperContext
    {
        //private readonly IConfiguration _configuration;
        //private readonly string _connectionString;
        //public DapperContext(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    _connectionString = _configuration.GetConnectionString("AppDbContext");
        //}
        //public IDbConnection CreateConnection()
        //    => new SqlConnection(_connectionString);

        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
            => new SqlConnection(_configuration.GetConnectionString("AppDbContext"));
        public IDbConnection CreateMasterConnection()
            => new SqlConnection(_configuration.GetConnectionString("MasterConnection"));


    }
}
