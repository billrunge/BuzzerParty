using System.Threading.Tasks;
using BuzzerPartyInterfaces;
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
            IDatabase database = new MsSqlDatabase() 
            { 
                SqlConnectionString = sqlConnectionString.GetSqlConnectionString() 
            };

            await database.DropAllTablesAsync();
            return "Database Cleaned Successfully";
        }
    }
}