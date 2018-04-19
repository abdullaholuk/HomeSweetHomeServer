using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel> GetByIdAsync(int id);
        Task<UserModel> GetByUsernameAsync(string username);
        Task<List<UserModel>> GetAllAsync();
    }
}
