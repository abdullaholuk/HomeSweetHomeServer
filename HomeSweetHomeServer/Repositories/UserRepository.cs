using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HomeSweetHomeServer.Repositories
{
    public class UserRepository : BaseRepository<AuthenticationModel>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<AuthenticationModel>> GetAllAsync()
        {
            return await Db.ToListAsync();
        }

        public async Task<AuthenticationModel> GetByIdAsync(int id)
        {
            return await Db.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AuthenticationModel> GetByUsernameAsync(string username)
        {
            return await Db.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
