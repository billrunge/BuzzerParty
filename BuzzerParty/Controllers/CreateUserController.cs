using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BuzzerPartyLibrary;
using BuzzerParty.Models;

namespace BuzzerParty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateUserController : ControllerBase
    {
        // POST api/CreateUser
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserViewModel createUserViewModel)
        {
            string userName = createUserViewModel.Name;
            string gameCode = createUserViewModel.GameCode.ToUpper();
            SqlConnectionString sqlConnectionString = new SqlConnectionString();
            string connectionString = sqlConnectionString.GetSqlConnectionString();

            Game gameHelper = new Game()
            {
                SqlConnectionString = connectionString
            };

            User userHelper = new User()
            {
                SqlConnectionString = connectionString
            };

            JWT jwtHelper = new JWT();

            int game = await gameHelper.GetGameFromCodeAsync(gameCode);

            string jwt = await jwtHelper.GenerateJwtAsync(await userHelper.CreateUserAsync(userName, game, false),
                        game,
                        userName,
                        gameCode,
                        false,
                        Environment.GetEnvironmentVariable("JWT_KEY"));

            var returnObject = new { JWT = jwt };

            return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(returnObject));
        }
    }
}
