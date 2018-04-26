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

        public async Task<List<UserModel>> GetAllAsync(bool include = false)
        {
            if (include == false)
                return await Db.ToListAsync();
            else
                return await Db.Include(u => u.Home).ToListAsync();
        }
                
        public async Task<UserModel> GetByIdAsync(int id, bool include = false)
        {
            if(include == false)
                return await Db.SingleOrDefaultAsync(u => u.Id == id);
            else
                return await Db.Include(u => u.Home).SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserModel> GetByUsernameAsync(string username, bool include = false)
        {
            if (include == false)
                return await Db.SingleOrDefaultAsync(u => u.Username == username);
            else
                return await Db.Include(u => u.Home).SingleOrDefaultAsync(u => u.Username == username);
        }
    }
}
