using System.Threading.Tasks;

namespace BuzzerPartyInterfaces
{
    public interface IDatabase
    {
        string SqlConnectionString { get; set; }
        Task DropAllTablesAsync();
        Task CreateSchemaAsync();
    }
}