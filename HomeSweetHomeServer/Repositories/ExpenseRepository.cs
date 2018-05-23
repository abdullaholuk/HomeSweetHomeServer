using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeSweetHomeServer.Repositories
{
    public class ExpenseRepository : BaseRepository<ExpenseModel>, IExpenseRepository
    {
        public ExpenseRepository(DatabaseContext context) : base(context)
        {
        }

        //Gets expense by expense id
        public async Task<ExpenseModel> GetExpenseByIdAsync(int id, bool include = false)
        {
            if (include == false)
                return await Db.SingleOrDefaultAsync(e => e.Id == id);
            else
                return await Db.Include(e => e.Author).Include(e => e.Home).SingleOrDefaultAsync(e => e.Id == id);
        }

        //Gets all home expenses
        public async Task<List<ExpenseModel>> GetAllExpensesByHomeId(int homeId, bool include = false)
        {
            if (include == false)
                return await Db.Where(e => e.Home.Id == homeId).ToListAsync();
            else
                return await Db.Include(e => e.Author).Include(e => e.Home).Where(e => e.Home.Id == homeId).ToListAsync();
        }
    }
}
