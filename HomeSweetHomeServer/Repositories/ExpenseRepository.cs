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
        public async Task<ExpenseModel> GetExpenseById(int id)
        {
            return await Db.SingleOrDefaultAsync(e => e.Id == id);
        }
    }
}
