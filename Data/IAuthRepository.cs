using System.Threading.Tasks;
using dotnet.rpg.Models;

namespace dotnet.rpg.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponce<int>> Register(User user, string password);
    
        Task<ServiceResponce<string>> Login(string username, string password);

        Task<bool> UserExists(string username);
    }
}