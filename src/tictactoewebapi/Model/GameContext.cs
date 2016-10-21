using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;

namespace tictactoewebapi.Model
{
    public class GameContext : ModelEntity
    {
        public GameContext() { }
        
        public string opponent { get; set; }
        public string initiator { get; set; }
        public string turn { get; set; }

        public bool complete { get; set; }
        public string winner { get; set; }

        public DateTime? completed { get; set; }

    }
}
