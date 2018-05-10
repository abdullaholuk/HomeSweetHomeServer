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

        public async Task<ShoppingListModel> GetShoppingListItemById(int homeId, int materialId, bool include = false)
        {
            if (include == false)
                return await Db.SingleOrDefaultAsync(s => s.Home.Id == homeId && s.Material.Id == materialId);
            else
                return await Db.Include(s => s.Home).ThenInclude(h => h.Users).Include(s => s.Material).
                                SingleOrDefaultAsync(s => s.Home.Id == homeId && s.Material.Id == materialId);
        }
    }
}
