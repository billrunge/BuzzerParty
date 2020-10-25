using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuzzerPartyInterfaces
{
    public interface IBuzz
    {
        string SqlConnectionString { get; set; }
        Task BuzzAsync(int user, int question);
    }
}
