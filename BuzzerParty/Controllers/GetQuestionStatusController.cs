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
    public class GetQuestionStatusController : ControllerBase
    {
        // POST api/GetQuestionStatus
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GameSession gameSession)
        {
            SqlConnectionString sqlConnectionString = new SqlConnectionString();
            Question questionHelper = new Question() 
            { 
                SqlConnectionString = sqlConnectionString.GetSqlConnectionString()
        };

            JWT jwtHelper = new JWT();

            Question.QuestionStatus questionStatus = 
                await questionHelper.GetQuestionStatusFromUserAsync(jwtHelper.GetUserFromJWT(gameSession.JWT));

            var returnObject = new 
            { 
                Question = questionStatus.question, 
                Answerable = questionStatus.answerable, 
                UserBuzzed = questionStatus.userBuzzed 
            };

            return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(returnObject));
        }
    }
}
