using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    //Interface about meal repository operations
    public interface IMealRepository : IBaseRepository<MealModel>
    {
        Task<MealModel> GetHomeMealByName(int homeId, string name, bool include = false);
        Task<MealModel> GetHomeMealById(int mealId, bool include = false);
    }
}
