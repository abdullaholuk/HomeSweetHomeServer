using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    public interface IMaterialRepository : IBaseRepository<MaterialModel>
    {
        Task<MaterialModel> GetMaterialByName(string name);
        Task<MaterialModel> GetMaterialBId(int id);
    }
}
