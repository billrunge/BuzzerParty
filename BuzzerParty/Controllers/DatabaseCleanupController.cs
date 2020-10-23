using System.Threading.Tasks;
using BuzzerPartyLibrary;
using Microsoft.AspNetCore.Mvc;

namespace BuzzerParty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseCleanupController : ControllerBase
    {
        // GET api/DatabaseCleanup
        [HttpGet]
        public async Task<string> Get()
        {
            SqlConnectionString sqlConnectionString = new SqlConnectionString();
            Database database = new Database() 
            { 
                SqlConnectionString = sqlConnectionString.GetSqlConnectionString() 
            };

            await database.DropAllTablesAsync();
            return "Database Cleaned Successfully";
        }
    }
}