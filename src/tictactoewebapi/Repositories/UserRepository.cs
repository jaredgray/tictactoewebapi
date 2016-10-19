using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using tictactoewebapi.Model;
using Microsoft.WindowsAzure.Storage.Table;

namespace tictactoewebapi.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IOptions<ConfigurationOptions> configuration) : base(configuration)
        {
        }
        public UserRepository() { }
        
        public async Task<User> CreateUser(User value)
        {
            var cloudTable = await base.GetTableAsync("User");
            value.PartitionKey = "game-tictactoe";
            value.RowKey = value.email;
            value.created = DateTime.Now;
            TableOperation insertOperation = TableOperation.Insert(value);
            // Create the InsertOrReplace TableOperation.
            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(value);
            // Execute the operation.
            var insertOrReplaceResult = await cloudTable.ExecuteAsync(insertOrReplaceOperation);
            return (User)insertOrReplaceResult.Result;
        }
        public async Task<User> ByEmail(string email)
        {
            CloudTable cloudTable = await base.GetTableAsync("User");
            TableOperation retrieveOperation = TableOperation.Retrieve<User>("game-tictactoe", email);
            var retrieveResult = await cloudTable.ExecuteAsync(retrieveOperation);
            return (User)retrieveResult.Result;
        }
    }
}
