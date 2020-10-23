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
    public class CreateGameController : ControllerBase
    {
        // POST api/CreateGame
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateGameViewModel createGameViewModel)
        {
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

            Database database = new Database() 
            { 
                SqlConnectionString = connectionString
            };

            JWT jwtHelper = new JWT();

            string userName = createGameViewModel.Name;

            await database.CreateSchemaAsync();

            string gameCode = gameHelper.GenerateGameCode(5);
            int game = await gameHelper.CreateGameAsync(gameCode);


            string jwt = await jwtHelper.GenerateJwtAsync(
                            await userHelper.CreateUserAsync(userName, game, true),
                            game,
                            userName,
                            gameCode,
                            true,
                            Environment.GetEnvironmentVariable("JWT_KEY"));

            return new OkObjectResult(JsonConvert.SerializeObject(new { JWT = jwt }));
        }
    }
}