using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;


namespace HomeSweetHomeServer.Repositories
{
    public interface IShoppingListRepository : IBaseRepository<ShoppingListModel>
    {
        Task<ShoppingListModel> GetHomeShoppingListByIdAsync(int homeId, bool include = false);
    }
}
