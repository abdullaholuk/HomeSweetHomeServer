using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    //Interface about menu repository operations
    public interface IMenuRepository : IBaseRepository<MenuModel>
    {
        Task<MenuModel> GetHomeMenuByDate(int homeId, DateTime date, bool include = false);
    }
}
