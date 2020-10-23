using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            Game gameHelper = new Game()
            {
                SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            };

            User userHelper = new User()
            {
                SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            };

            Database database = new Database() { SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING") };

            JWT jwtHelper = new JWT();

            string userName = createGameViewModel.Name;

            await database.CreateSchemaAsync();

            string gameCode = gameHelper.GenerateGameCode(5);
            int game = await gameHelper.CreateGameAsync(gameCode);

            int user = await userHelper.CreateUserAsync(userName, game, true);

            string jwt = await jwtHelper.GenerateJwtAsync(user,
                            game,
                            userName,
                            gameCode,
                            true,
                            Environment.GetEnvironmentVariable("JWT_KEY"));

            var returnObject = new { JWT = jwt };

            return new OkObjectResult(JsonConvert.SerializeObject(returnObject));

            //return new OkObjectResult("Test");
        }
    }
}