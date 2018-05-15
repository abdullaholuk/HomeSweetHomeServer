using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;

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
                return await Db.Include(e => e.Author).SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
