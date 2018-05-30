using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace HomeSweetHomeServer.Repositories
{
    public class MenuRepository : BaseRepository<MenuModel>, IMenuRepository
    {
        public MenuRepository(DatabaseContext context) : base(context)
        {
        }

        //Gets home menu by date
        public async Task<MenuModel> GetHomeMenuByDate(int homeId, DateTime date, bool include = false)
        {
            if (include == false)
                return await Db.SingleOrDefaultAsync(m => m.Home.Id == homeId && m.Date == date);
            else
                return await Db.Include(m => m.Home).SingleOrDefaultAsync(m => m.Home.Id == homeId && m.Date == date);
        }
    }
}
