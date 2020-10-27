using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using BuzzerPartyLibrary;
using BuzzerParty.Models;
using BuzzerPartyInterfaces;

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
            MsSqlQuestion questionHelper = new MsSqlQuestion()
            {
                SqlConnectionString = sqlConnectionString.GetSqlConnectionString()
            };

            JWT jwtHelper = new JWT();

            IQuestionStatus questionStatus =
                await questionHelper.GetQuestionStatusFromUserAsync(jwtHelper.GetUserFromJWT(gameSession.JWT));

            var returnObject = new
            {
                questionStatus.Question,
                questionStatus.Answerable,
                questionStatus.UserBuzzed
            };

            return (ActionResult)new OkObjectResult(JsonConvert.SerializeObject(returnObject));
        }
    }
}