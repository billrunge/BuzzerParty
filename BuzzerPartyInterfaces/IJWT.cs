using System.Threading.Tasks;

namespace BuzzerPartyInterfaces
{
    public interface IJWT
    {
        Task<string> GenerateJwtAsync(int user, int game, string userName, string gameCode, bool isAlex, string key);
        int GetUserFromJWT(string jwt);
        int GetGameFromJWT(string jwt);
    }
}
