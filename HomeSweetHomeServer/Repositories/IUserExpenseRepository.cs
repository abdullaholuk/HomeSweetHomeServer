using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    //Interface about user expense operations
    public interface IUserExpenseRepository : IBaseRepository<UserExpenseModel>
    {
        Task<List<UserExpenseModel>> GetAllUserExpenseByUserId(int userId, bool include = false);
        Task<List<UserExpenseModel>> GetAllUserExpenseByExpenseId(int expenseId, bool include = false);
        Task<UserExpenseModel> GetUserExpenseById(int id, bool include = false);
    }
}
