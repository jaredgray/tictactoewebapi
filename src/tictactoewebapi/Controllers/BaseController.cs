using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tictactoewebapi.Model;

namespace tictactoewebapi.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(IOptions<ConfigurationOptions> configuration)
        {
            this.Configuration = configuration.Value;
        }
        public ConfigurationOptions Configuration { get; set; }
        protected async Task<CloudTable> GetTableAsync(string tableName)
        {
            //// Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Configuration.AzureConnection);
            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            // Retrieve a reference to the table.
            CloudTable cloudTable = tableClient.GetTableReference(tableName);
            // Create the table if it doesn't exist.
            var result = await cloudTable.CreateIfNotExistsAsync();

            return cloudTable;
        }
    }
}
