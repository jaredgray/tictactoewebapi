using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tictactoewebapi.Model;
using tictactoewebapi.Repositories;

namespace tictactoewebapi.Controllers
{
    public class BaseController<TRepository> : Controller
        //where TRepository : IBaseRepository
    {
        public BaseController(IOptions<ConfigurationOptions> options, TRepository repository)
        {
            Repository = repository;
            Configuration = options.Value;
        }
        public TRepository Repository { get; set; }
        public ConfigurationOptions Configuration { get; set; }
        protected async Task<CloudTable> GetTableAsync(string tableName)
        {
            //// Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.AzureConnection);
            
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Retrieve a reference to the table.
            CloudTable cloudTable = tableClient.GetTableReference(tableName);
            try
            {
                // Create the table if it doesn't exist.
                var result = await cloudTable.CreateIfNotExistsAsync();

                return cloudTable;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
