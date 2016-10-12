using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace tictactoewebapi.Model
{
    public class ModelEntity : TableEntity
    {
        public ModelEntity(string partitionKey) : base(partitionKey, string.Empty) { }
        public ModelEntity() { }

        public void CreateId()
        {
            this.created = DateTime.Now;
            this.RowKey = string.Format("{0:yyyyMMdd}{1}", this.created, Guid.NewGuid().ToString());
        }

        public DateTime? created { get; set; }
    }
}
