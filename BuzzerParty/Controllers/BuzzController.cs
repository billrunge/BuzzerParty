using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuzzerPartyLibrary;
using Newtonsoft.Json;
using BuzzerParty.Models;
using Microsoft.AspNetCore.SignalR;

namespace BuzzerParty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuzzController : ControllerBase
    {
        private readonly IHubContext<BuzzerSignalR> _hubContext;
        public BuzzController(IHubContext<BuzzerSignalR> hubContext)
        {
            _hubContext = hubContext;
        }
        // POST api/Buzz
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameSession gameSession)
        {
            Question questionHelper = new Question()
            {
                SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            };

            User userHelper = new User()
            {
                SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            };

            JWT jwtHelper = new JWT();

            Buzz buzzHelper = new Buzz()
            {
                SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            };

            int user = jwtHelper.GetUserFromJWT(gameSession.JWT);
            Question.QuestionStatus questionStatus = 
                await questionHelper.GetQuestionStatusFromUserAsync(jwtHelper.GetUserFromJWT(gameSession.JWT));

            await buzzHelper.BuzzAsync(user, questionStatus.question);
            await _hubContext.Clients.All.SendAsync(
                $"User{await userHelper.GetAlexFromQuestionAsync(questionStatus.question)}", 
                await userHelper.GetUserNameFromUserAsync(user));

            return new OkObjectResult(JsonConvert.SerializeObject(new { Success = true }));
        }
    }
}