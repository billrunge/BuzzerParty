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
    public interface IQuestionStatus
    {
        int currentQuestion { get; set; }
        int questionCount { get; set; }
        int question { get; set; }
        bool answerable { get; set; }
        bool userBuzzed { get; set; }
    }
}