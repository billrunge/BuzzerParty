using System.Threading.Tasks;
using BuzzerPartyLibrary;
using Microsoft.AspNetCore.Mvc;

namespace BuzzerParty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseCleanupController : ControllerBase
    {
        // POST api/Buzz
        [HttpGet]
        public async Task Get()
        {
            SqlConnectionString sqlConnectionString = new SqlConnectionString();
            Database database = new Database() 
            { 
                SqlConnectionString = sqlConnectionString.GetSqlConnectionString() 
            };

            await database.DropAllTablesAsync();
        }
    }
}