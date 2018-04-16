using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    public interface IUserRepository : IBaseRepository<AuthenticationModel>
    {
        Task<AuthenticationModel> GetByIdAsync(int id);
        Task<AuthenticationModel> GetByUsernameAsync(string username);
        Task<List<AuthenticationModel>> GetAllAsync();
    }
}
