using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Services
{
    //Interface for token operations
    public interface IJwtTokenService
    {
        string CreateToken(UserModel user);
     //   bool VerifyToken(string token);
    }
}
