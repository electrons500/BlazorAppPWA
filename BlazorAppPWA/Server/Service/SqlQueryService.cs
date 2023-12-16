using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace BlazorAppPWA.Server.Service
{
    public class SqlQueryService
    {
        private GamesDBContext.GamesDbContext _context;
        private IConfiguration _configuration;
        public SqlQueryService(GamesDBContext.GamesDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> ExecuteSqlQueryFromLocalDB(string sqlquery)
        {
            int i = 0;     
            using (var con = new SqlConnection(_configuration.GetConnectionString("Conn")))
            {
              i = await con.ExecuteAsync(sqlquery);
            }
            return i > 0;
        }
    }
    
}
