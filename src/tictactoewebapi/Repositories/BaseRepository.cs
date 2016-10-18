using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tictactoewebapi.Model;

namespace tictactoewebapi.Repositories
{
    public class BaseRepository 
    {
        //public BaseRepository(IOptions<ConfigurationOptions> configuration)
        //{
        //    this.Configuration = configuration.Value;
        //}
        public BaseRepository() { }
        public ConfigurationOptions Configuration { get; set; }
        public async Task<CloudTable> GetTableAsync(string tableName)
        {
            var connection = Configuration?.AzureConnection;
            if (null == connection)
                connection = "DefaultEndpointsProtocol=https;AccountName=wstus;AccountKey=x98xoCSBs54j/pCUtxiRno3uGENRg4dqAxVSTTo18HaUmMHmwaOFxXHoA+Y+CRDbGMg7g6B7Lm1smD9Dp1dmgw==";
            //// Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connection);

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
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
