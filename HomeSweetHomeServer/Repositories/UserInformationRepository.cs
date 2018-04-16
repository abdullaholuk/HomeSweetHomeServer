using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeSweetHomeServer.Repositories
{
    public class UserInformationRepository : BaseRepository<UserInformationModel>, IUserInformationRepository
    {
        public UserInformationRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<UserInformationModel> GetUserInformationByValueAsync(string value)
        {
            return await Db.SingleOrDefaultAsync(ui => ui.Value == value);
        }

        public async Task<List<UserInformationModel>> GetUserInformationModelsByUserIdAsync(int userId)
        {
            return await Db.Where(ui => ui.User.Id == userId).ToListAsync();
        }
    }
}
