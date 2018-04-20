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
        IUserRepository _userRepository;
        IInformationRepository _informationRepository;
        public UserInformationRepository(DatabaseContext context, IUserRepository userRepository, IInformationRepository informationRepository) : base(context)
        {
            _userRepository = userRepository;
            _informationRepository = informationRepository;
        }
        
        public async Task<UserInformationModel> GetUserInformationByValueAsync(string value)
        {
            
           return await Db.Include(u => u.User).Include(i => i.Information).SingleOrDefaultAsync(ui => ui.Value == value);
        }

        public async Task<List<UserInformationModel>> GetAllUserInformationsByUserIdAsync(int userId)
        {
            return await Db.Where(ui => ui.User.Id == userId).ToListAsync();
        }

        public async Task<UserInformationModel> GetUserInformationByIdAsync(int userId, int informationId)
        {
            return await Db.SingleOrDefaultAsync(ui => (ui.User.Id == userId && ui.Information.Id == informationId));
        }
    }
}
