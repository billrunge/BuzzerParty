using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BuzzerPartyLibrary;
using BuzzerParty.Models;
using Microsoft.AspNetCore.SignalR;

namespace BuzzerParty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakeQuestionAnswerableController : ControllerBase
    {
        private static readonly HttpClient _client = new HttpClient();
        private static int game;
        private static int question;
        private static string jwt;
        private readonly IHubContext<BuzzerSignalR> _hubContext;

        public MakeQuestionAnswerableController(IHubContext<BuzzerSignalR> hubContext)
        {
            _hubContext = hubContext;
        }

        // POST api/MakeQuestionAnswerable
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MakeQuestionAnswerableViewModel makeQuestionAnswerableViewModel)
        {
            jwt = makeQuestionAnswerableViewModel.JWT;

            JWT jwtHelper = new JWT();
            Question questionHelper = new Question()
            {
                SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            };

            game = jwtHelper.GetGameFromJWT(jwt);

            question = await questionHelper.GetQuestionFromGameAsync(game);

            await questionHelper.MakeQuestionAnswerableAsync(question);
            await _hubContext.Clients.All.SendAsync($"Game{game}", "Answerable");

            var returnObject = new { Status = "Question is now Answerable" };

            return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(returnObject));
        }
    }
}