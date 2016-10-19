using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace tictactoewebapi.Containers
{
    public class TimeoutContainer
    {
        public TimeoutContainer()
        {

        }

        //public static Task<ContainerResult<TResult>> TryWithTimeout<TResult>(Func<TResult> func, int timeoutInSeconds)
        //{
        //    CancellationTokenSource src = new CancellationTokenSource();            
        //    var result = Task.Factory.StartNew<TResult>(func, src.Token);

        //    DateTime start = DateTime.Now;
        //    var end = start.AddSeconds(timeoutInSeconds);

        //    while(start <= end)
        //    {
                
        //    }
            
        //}
    }
}
