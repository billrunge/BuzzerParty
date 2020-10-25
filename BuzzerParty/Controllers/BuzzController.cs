using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuzzerPartyLibrary;
using Newtonsoft.Json;
using BuzzerParty.Models;
using Microsoft.AspNetCore.SignalR;
using BuzzerPartyInterfaces;

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
            SqlConnectionString sqlConnectionString = new SqlConnectionString();
            string connectionString = sqlConnectionString.GetSqlConnectionString();

            IQuestion questionHelper = new Question()
            {
                SqlConnectionString = connectionString
            };

            IUser userHelper = new User()
            {
                SqlConnectionString = connectionString
            };

            IJWT jwtHelper = new JWT();

            IBuzz buzzHelper = new Buzz()
            {
                SqlConnectionString = connectionString
            };

            int user = jwtHelper.GetUserFromJWT(gameSession.JWT);
            IQuestionStatus questionStatus = 
                await questionHelper.GetQuestionStatusFromUserAsync(jwtHelper.GetUserFromJWT(gameSession.JWT));

            await buzzHelper.BuzzAsync(user, questionStatus.question);
            await _hubContext.Clients.All.SendAsync(
                $"User{await userHelper.GetAlexFromQuestionAsync(questionStatus.question)}", 
                await userHelper.GetUserNameFromUserAsync(user));

            return new OkObjectResult(JsonConvert.SerializeObject(new { Success = true }));
        }
    }
}