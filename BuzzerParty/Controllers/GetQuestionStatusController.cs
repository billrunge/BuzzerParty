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
    public class GetQuestionStatusController : ControllerBase
    {
        // POST api/GetQuestionStatus
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GetQuestionStatusViewModel getQuestionStatusViewModel)
        {
            string jwt = getQuestionStatusViewModel.JWT;

            Question questionHelper = new Question() { SqlConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING") };
            JWT jwtHelper = new JWT();

            Question.QuestionStatus questionStatus = await questionHelper.GetQuestionStatusFromUserAsync(jwtHelper.GetUserFromJWT(jwt));

            var returnObject = new { Question = questionStatus.question, Answerable = questionStatus.answerable, UserBuzzed = questionStatus.userBuzzed };

            return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(returnObject));
        }
    }
}
