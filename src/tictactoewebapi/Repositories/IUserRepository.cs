using System.Threading.Tasks;
using tictactoewebapi.Model;

namespace tictactoewebapi.Repositories
{
    public interface IUserRepository : IBaseRepository
    {
        Task<User> ByEmail(string email);
        Task<User> CreateUser(User value);
    }
}