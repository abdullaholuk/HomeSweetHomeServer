using System.Collections.Generic;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSweetHomeServer.Repositories
{
    public class UserExpenseRepository : BaseRepository<UserExpenseModel>, IUserExpenseRepository
    {
        public UserExpenseRepository(DatabaseContext context) : base(context)
        {
        }

        //Gets all user expenses by user id
        public async Task<List<UserExpenseModel>> GetAllUserExpenseByUserId(int userId, bool include = false)
        {
            if (include == false)
                return await Db.Where(ue => ue.User.Id == userId).ToListAsync();
            else
                return await Db.Include(ue => ue.User).ThenInclude(u => u.Home).
                    ThenInclude(h => h.Users).Include(ue => ue.Expense).Where(ue => ue.User.Id == userId).ToListAsync();
        }

        //Gets all user expenses by expense id
        public async Task<List<UserExpenseModel>> GetAllUserExpenseByExpenseId(int expenseId, bool include = false)
        {
            if (include == false)
                return await Db.Where(ue => ue.User.Id == expenseId).ToListAsync();
            else
                return await Db.Include(ue => ue.User).ThenInclude(u => u.Home).
                    ThenInclude(h => h.Users).Include(ue => ue.Expense).Where(ue => ue.Expense.Id == expenseId).ToListAsync();
        }

        //Gets specific user expense by user expense id
        public async Task<UserExpenseModel> GetUserExpenseById(int id, bool include = false)
        {
            if (include == false)
                return await Db.SingleOrDefaultAsync(ue => ue.Id == id);
            else
                return await Db.Include(ue => ue.User).ThenInclude(u => u.Home).
                    ThenInclude(h => h.Users).Include(ue => ue.Expense).SingleOrDefaultAsync(ue => ue.Id == id);
        }
    }
}
