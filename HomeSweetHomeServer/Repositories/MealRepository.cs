using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeSweetHomeServer.Repositories
{
    public class MealRepository : BaseRepository<MealModel>, IMealRepository
    {
        public MealRepository(DatabaseContext context) : base(context)
        {
        }

        //Gets home meal by name
        public async Task<MealModel> GetHomeMealByName(int homeId, string name, bool include = false)
        {
            if (include == false)
                return await Db.SingleOrDefaultAsync(m => m.Home.Id == homeId && m.Name == name);
            else
                return await Db.Include(m => m.Home).SingleOrDefaultAsync(m => m.Home.Id == homeId && m.Name == name);
        }

        //Gets home meal by id
        public async Task<MealModel> GetHomeMealById(int mealId, bool include = false)
        {
            if (include == false)
                return await Db.SingleOrDefaultAsync(m => m.Id == mealId);
            else
                return await Db.Include(m => m.Home).SingleOrDefaultAsync(m => m.Id == mealId);
        }
    }
}
