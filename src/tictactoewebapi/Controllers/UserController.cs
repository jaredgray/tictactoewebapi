using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using Microsoft.WindowsAzure.Storage.Table.Protocol;
using Microsoft.Extensions.Configuration;
using tictactoewebapi.Model;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;
using tictactoewebapi.Repositories;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace tictactoewebapi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController<IUserRepository>
    {
        public UserController(IOptions<ConfigurationOptions> configuration, IUserRepository repo)
            : base(configuration, repo)
        {
        }

        // GET: api/User
        /// <summary>
        /// gets nothing
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { };
        }

        // GET api/User/jared
        /// <summary>
        /// gets a list of Users that a person has played
        /// </summary>
        /// <param name="userName">the user to get the Users of</param>
        /// <returns></returns>
        [HttpGet("{userName}")]
        public async Task<IEnumerable<User>> GetAll(string userName)
        {
            //// Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.AzureConnection);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Retrieve a reference to the table.
            CloudTable cloudTable = tableClient.GetTableReference("User");
            // Create the table if it doesn't exist.
            var result = await cloudTable.CreateIfNotExistsAsync();

            return null;
        }

        /// <summary>
        /// gets a User context by the rowkey
        /// </summary>
        /// <param name="UserKey"></param>
        /// <returns></returns>
        [HttpGet("{UserKey}")]
        public async Task<User> Get(string UserKey)
        {
            //// Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.AzureConnection);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Retrieve a reference to the table.
            CloudTable cloudTable = tableClient.GetTableReference("User");
            // Create the table if it doesn't exist.
            var result = await cloudTable.CreateIfNotExistsAsync();

            // Define the query, and select only the Email property.
            TableQuery<DynamicTableEntity> projectionQuery = new TableQuery<DynamicTableEntity>().Select(new string[] { UserKey });

            // Define an entity resolver to work with the entity after retrieval.
            EntityResolver<string> resolver = (pk, rk, ts, props, etag) => rk == UserKey ? rk : null;

            //foreach (string projectedEmail in cloudTable.(projectionQuery, resolver, null, null))
            //{
            //    Console.WriteLine(projectedEmail);
            //}

            return null;
        }

        /// <summary>
        /// gets a User context by the rowkey
        /// </summary>
        /// <param name="UserKey"></param>
        /// <returns></returns>
        [HttpGet("{email}")]
        public async Task<User> ByEmail(string email)
        {
            return await this.Repository.ByEmail(email);
        }

        /// <summary>
        /// create or update a user
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST api/User
        [HttpPost]
        public async Task<User> Post([FromBody] User value)
        {
            var existing = await ByEmail(value.email);
            if (null == existing)
            {
                value = await this.Repository.CreateUser(value);
                return value;
            }
            else
            {
                value = existing.UpdateWith(value);
                // Create the InsertOrReplace TableOperation.
                TableOperation insertOrReplaceOperation = TableOperation.InsertOrReplace(value);
                var cloudTable = await base.GetTableAsync("User");
                // Execute the operation.
                var insertOrReplaceResult = await cloudTable.ExecuteAsync(insertOrReplaceOperation);
                return (User)insertOrReplaceResult.Result;
            }
        }

        /// <summary>
        /// creates a user if it doesn't exist
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT api/User
        [HttpPut]
        public async Task<User> Put([FromBody] User value)
        {
            var existing = await ByEmail(value.email);
            if (null == existing)
            {
                return await this.Repository.CreateUser(value);
            }
            else
            {
                return existing;
            }
        }

        // DELETE api/values/userName
        [HttpDelete("{email}")]
        public void Delete(string email)
        {
        }
    }
}
