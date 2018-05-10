using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;


namespace HomeSweetHomeServer.Repositories
{
    public class ShoppingListRepository : BaseRepository<ShoppingListModel>, IShoppingListRepository
    {
        public ShoppingListRepository(DatabaseContext context) : base(context)
        {
        }
        /*
        public async Task<ShoppingListModel> GetHomeShoppingListByIdAsync(int homeId)
        {
        }*/
    }
}
