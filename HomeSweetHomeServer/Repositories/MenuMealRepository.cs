using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HomeSweetHomeServer.Repositories
{
    public class MenuMealRepository : BaseRepository<MenuMealModel>, IMenuMealRepository
    {
        public MenuMealRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
