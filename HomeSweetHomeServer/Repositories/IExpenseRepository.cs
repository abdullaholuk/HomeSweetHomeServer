using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    //Interface about expense operations
    public interface IExpenseRepository : IBaseRepository<ExpenseModel>
    {
        Task<ExpenseModel> GetExpenseByIdAsync(int id, bool include = false);
    }
}
