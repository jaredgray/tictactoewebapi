using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using tictactoewebapi.Model;

namespace tictactoewebapi.Repositories
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        public GameRepository(IOptions<ConfigurationOptions> configuration) : base(configuration)
        {
        }
    }
}
