using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HomeSweetHomeServer.Repositories
{
    public class MaterialRepository : BaseRepository<MaterialModel>, IMaterialRepository
    {
        public MaterialRepository(DatabaseContext context) : base(context)
        {
        }
        public async Task<MaterialModel> GetMaterialByName(string name)
        {
            return await Db.SingleOrDefaultAsync(m => m.MaterialName == name);
        }

        public async Task<MaterialModel> GetMaterialBId(int id)
        {
            return await Db.SingleOrDefaultAsync(m => m.Id == id);
        }
    }
}
