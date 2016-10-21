using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using tictactoewebapi.Model;
using Microsoft.WindowsAzure.Storage.Table;

namespace tictactoewebapi.Repositories
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(IOptions<ConfigurationOptions> configuration) : base(configuration)
        {
        }
        public GameRepository() { }

        public async Task<User> CreateGame(User value)
        {
            var cloudTable = await base.GetTableAsync("GameContext");
            
            if (string.IsNullOrEmpty(value.RowKey))
            {
                value.CreateId();
                TableOperation insertOperation = TableOperation.Insert(value);
            }
            // Create the InsertOrReplace TableOperation.
            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(value);
            // Execute the operation.
            var insertOrReplaceResult = await cloudTable.ExecuteAsync(insertOrReplaceOperation);

            return (User)insertOrReplaceResult.Result;
        }

        /// <summary>
        /// Gets all results where the user was either the initiator or the opponent
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<GameContext>> GetGamesByUser(User user)
        {
            CloudTable cloudTable = await base.GetTableAsync("GameContext");
            string q1 = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "game-tictactoe");
            string q2 = TableQuery.GenerateFilterCondition("initiator", QueryComparisons.Equal, user.email);
            string q3 = TableQuery.GenerateFilterCondition("opponent", QueryComparisons.Equal, user.email);
            string filter = TableQuery.CombineFilters(q1, TableOperators.And, TableQuery.CombineFilters(q2, TableOperators.Or, q3));
            // Construct the query operation for all customer entities where PartitionKey="Smith".
            TableQuery<GameContext> query = new TableQuery<GameContext>().Where(filter);
            List<GameContext> results = new List<GameContext>();
            // Print the fields for each customer.
            TableContinuationToken token = null;
            do
            {
                TableQuerySegment<GameContext> resultSegment = await cloudTable.ExecuteQuerySegmentedAsync(query, token);
                token = resultSegment.ContinuationToken;
                foreach (GameContext game in resultSegment.Results)
                    results.Add(game);
                
            } while (token != null);
            return results;
        }

        public async Task<GameContext> GetGame(string gameId)
        {
            CloudTable cloudTable = await base.GetTableAsync("GameContext");
            string q1 = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "game-tictactoe");
            string q2 = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, gameId);

            return null;
        }
    }
}
