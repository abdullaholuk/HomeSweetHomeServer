using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HomeSweetHomeServer.Repositories
{
    public class UserRepository : BaseRepository<UserModel>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await Db.ToListAsync();
        }
        
        public async Task<UserModel> GetByIdAsync(int id)
        {
            return await Db.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserModel> GetByUsernameAsync(string username)
        {
            return await Db.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
