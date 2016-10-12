using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using Microsoft.Extensions.Configuration;
using tictactoewebapi.Model;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace tictactoewebapi.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        public GameController(IOptions<ConfigurationOptions> configuration)
        {
            this.Configuration = configuration.Value;
        }
        public ConfigurationOptions Configuration { get; set; }
        // GET: api/game
        /// <summary>
        /// gets nothing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { };
        }

        // GET api/game/jared
        /// <summary>
        /// gets a list of Games that a person has played
        /// </summary>
        /// <param name="userName">the user to get the games of</param>
        /// <returns></returns>
        [HttpGet("{userName}")]
        public async Task<IEnumerable<GameContext>> GetAll(string userName)
        {
            //// Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.AzureConnection);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Retrieve a reference to the table.
            CloudTable cloudTable = tableClient.GetTableReference("gamecontext");
            // Create the table if it doesn't exist.
            var result = await cloudTable.CreateIfNotExistsAsync();

            return null;
        }
        
        /// <summary>
        /// gets a game context by the rowkey
        /// </summary>
        /// <param name="gameKey"></param>
        /// <returns></returns>
        [HttpGet("{gameKey}")]
        public async Task<GameContext> Get(string gameKey)
        {
            //// Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.AzureConnection);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Retrieve a reference to the table.
            CloudTable cloudTable = tableClient.GetTableReference("gamecontext");
            // Create the table if it doesn't exist.
            var result = await cloudTable.CreateIfNotExistsAsync();

            // Define the query, and select only the Email property.
            TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>().Select(new string[] { gameKey });

            // Define an entity resolver to work with the entity after retrieval.
            EntityResolver<string> resolver = (pk, rk, ts, props, etag) => props.ContainsKey("Email") ? props["Email"].StringValue : null;

            foreach (string projectedEmail in table.ExecuteQuery(projectionQuery, resolver, null, null))
            {
                Console.WriteLine(projectedEmail);
            }

            return null;
        }

        // GET api/game/new
        /// <summary>
        /// gets a new instance of a game
        /// </summary>
        /// <param name="userName">the user to create the game for</param>
        /// <returns></returns>
        [HttpGet]
        public GameContext New(string userName)
        {
            return new GameContext()
            {
                me = userName,
                initiator = userName
            };
        }

        // POST api/values
        [HttpPost]
        public async Task<GameContext> Post(GameContext value)
        {
            //// Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.AzureConnection);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Retrieve a reference to the table.
            CloudTable cloudTable = tableClient.GetTableReference("gamecontext");
            // Create the table if it doesn't exist.
            var result = await cloudTable.CreateIfNotExistsAsync();


            if(string.IsNullOrEmpty(value.RowKey))
            {
                value.CreateId();
                TableOperation insertOperation = TableOperation.Insert(value);
            }
            // Create the InsertOrReplace TableOperation.
            TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(value);
            // Execute the operation.
            var insertOrReplaceResult = await cloudTable.ExecuteAsync(insertOrReplaceOperation);

            return value;
        }


        // DELETE api/values/userName
        [HttpDelete("{userName}/{gameKey}")]
        public void Delete(string userName, string gameKey)
        {
        }
    }
}
