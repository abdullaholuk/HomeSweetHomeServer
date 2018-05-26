using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace HomeSweetHomeServer.Repositories
{
    public class MenuRepository : BaseRepository<MenuModel>, IMenuRepository
    {
        public MenuRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
