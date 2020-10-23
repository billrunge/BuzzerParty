using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BuzzerPartyLibrary;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
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
            //string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}";
            //await SendMessageAsync(alex, userName, baseUrl);
            await _hubContext.Clients.All.SendAsync($"User{alex}", userName);

            var returnObject = new { Success = true };

            return new OkObjectResult(JsonConvert.SerializeObject(returnObject));
        }
        //private static async Task SendMessageAsync(int alex, string userName, string baseUrl)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(
        //        new { UserID = "User" + alex, Message = userName }), Encoding.UTF8, "application/json");
        //    //await _client.PostAsync($"{Environment.GetEnvironmentVariable("BASE_URL")}/SignalR/SendMessage", content);
        //    await _client.PostAsync($"{baseUrl}/SignalR/SendMessage", content);
        //}
    }
}