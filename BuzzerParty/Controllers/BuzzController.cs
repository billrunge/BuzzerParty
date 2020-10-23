using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuzzerPartyLibrary;
using Newtonsoft.Json;
using System.Net.Http;
using BuzzerParty.Models;
using Microsoft.AspNetCore.SignalR;

namespace BuzzerParty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuzzController : ControllerBase
    {
        private static readonly HttpClient _client = new HttpClient();
        private readonly IHubContext<BuzzerSignalR> _hubContext;

        public BuzzController(IHubContext<BuzzerSignalR> hubContext)
        {
            _hubContext = hubContext;
        }

        // POST api/Buzz
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BuzzViewModel buzzViewModel)
        {
            string jwt = buzzViewModel.JWT;
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

            int user = jwtHelper.GetUserFromJWT(jwt);
            Question.QuestionStatus questionStatus = await questionHelper.GetQuestionStatusFromUserAsync(user);

            await buzzHelper.BuzzAsync(user, questionStatus.question);
            int alex = await userHelper.GetAlexFromQuestionAsync(questionStatus.question);
            string userName = await userHelper.GetUserNameFromUserAsync(user);
            await _hubContext.Clients.All.SendAsync($"User{alex}", userName);
            var returnObject = new { Success = true };

            return new OkObjectResult(JsonConvert.SerializeObject(returnObject));
        }
    }
}