using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BuzzerPartyLibrary;
using BuzzerParty.Models;
using Microsoft.AspNetCore.SignalR;
using BuzzerPartyInterfaces;

namespace BuzzerParty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateQuestionController : ControllerBase
    {
        private static int game;
        private static int question;
        private readonly IHubContext<BuzzerSignalR> _hubContext;

        public CreateQuestionController(IHubContext<BuzzerSignalR> hubContext)
        {
            _hubContext = hubContext;
        }

        // POST api/CreateQuestion
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameSession gameSession)
        {
            IJWT jwtHelper = new JWT();
            game = jwtHelper.GetGameFromJWT(gameSession.JWT);
            SqlConnectionString sqlConnectionString = new SqlConnectionString();

           IQuestion questionHelper = new Question()
            {
                SqlConnectionString = sqlConnectionString.GetSqlConnectionString()
            };

            question = await questionHelper.CreateQuestionAsync(game);
            await _hubContext.Clients.All.SendAsync($"Game{game}", "NewQuestion");

            var returnObject = new { Question = question };

            return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(returnObject));
        }
    }
}