using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;
using tictactoewebapi.Model;

namespace tictactoewebapi.Repositories
{
    public interface IBaseRepository
    {
        ConfigurationOptions Configuration { get; set; }

        Task<CloudTable> GetTableAsync(string tableName);
    }
}