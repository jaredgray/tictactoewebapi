using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tictactoewebapi.Containers
{
    public class ContainerResult<TResult>
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public TResult Result { get; set; }
    }
}
