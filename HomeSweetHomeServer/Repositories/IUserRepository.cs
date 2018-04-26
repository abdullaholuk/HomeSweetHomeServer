using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    public interface IUserRepository : IBaseRepository<UserModel>
    {
        Task<UserModel> GetByIdAsync(int id, bool include = false);
        Task<UserModel> GetByUsernameAsync(string username, bool include = false);
        Task<List<UserModel>> GetAllAsync(bool include = false);
    }
}
