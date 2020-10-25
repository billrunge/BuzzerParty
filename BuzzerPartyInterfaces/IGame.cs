using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuzzerPartyInterfaces
{
    public interface IGame
    {
        string SqlConnectionString { get; set; }
        Task<int> GetGameFromCodeAsync(string gameCode);
        Task<int> CreateGameAsync(string gameCode);
        string GenerateGameCode(int size);
    }
}
