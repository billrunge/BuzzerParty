using System.Threading.Tasks;

namespace BuzzerPartyInterfaces
{
    public interface IUser
    {
        string SqlConnectionString { get; set; }
        Task<int> CreateUserAsync(string name, int game, bool isAlex);
        Task<string> GetUserNameFromUserAsync(int user);
        Task<int> GetAlexFromQuestionAsync(int question);
    }
}
