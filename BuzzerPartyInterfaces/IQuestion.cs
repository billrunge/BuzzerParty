using System.Threading.Tasks;

namespace BuzzerPartyInterfaces
{
    public interface IQuestion
    {
        string SqlConnectionString { get; set; }
        Task<IQuestionStatus> GetQuestionStatusFromUserAsync(int user);
        Task<int> GetQuestionFromGameAsync(int game);
        Task MakeQuestionAnswerableAsync(int question);
        Task<int> CreateQuestionAsync(int game);
    }
}