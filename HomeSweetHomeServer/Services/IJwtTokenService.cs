using HomeSweetHomeServer.Models;
using System.Threading.Tasks;

namespace HomeSweetHomeServer.Services
{
    //Interface for token operations
    public interface IJwtTokenService
    {
        string CreateToken(UserModel user);
        bool VerifyToken(string token);
        Task<UserModel> GetUserFromTokenStr(string tokenstr);
    }
}
