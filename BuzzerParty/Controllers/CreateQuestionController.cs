using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BuzzerPartyLibrary;
using System.Net.Http;
using System.Text;
using BuzzerParty.Models;
using Microsoft.AspNetCore.SignalR;

namespace BuzzerParty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateQuestionController : ControllerBase
    {
        private static readonly HttpClient _client = new HttpClient();
        private static int game;
        private static int question;
        private static string jwt;
        private readonly IHubContext<BuzzerSignalR> _hubContext;

        public CreateQuestionController(IHubContext<BuzzerSignalR> hubContext)
        {
            _hubContext = hubContext;
        }

        // POST api/CreateQuestion
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateQuestionViewModel createQuestionViewModel)
        {
            jwt = createQuestionViewModel.JWT;

            JWT jwtHelper = new JWT();
            game = jwtHelper.GetGameFromJWT(jwt);

            Question questionHelper = new Question()
            {
                SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            };

            question = await questionHelper.CreateQuestionAsync(game);
            //string baseUrl = $"{this.Request.Scheme}://{this.Request.Host}";
            //await SendMessageAsync(baseUrl);
            await _hubContext.Clients.All.SendAsync($"Game{game}", "NewQuestion");

            var returnObject = new { Question = question };

            return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(returnObject));
        }
        //private static async Task SendMessageAsync(string baseUrl)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(
        //        new { UserID = $"Game{game}", Message = "NewQuestion" }), Encoding.UTF8, "application/json");

        //    await _client.PostAsync($"{baseUrl}/SignalR/SendMessage", content);
        //}
    }
}