using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HomeSweetHomeServer.Repositories
{
    public class InformationRepository : BaseRepository<InformationModel>, IInformationRepository
    {
        public InformationRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<InformationModel>> GetAllAsync()
        {
            return await Db.ToListAsync();
        }

        public async Task<InformationModel> GetInformationByInformationNameAsync(string name)
        {
            return await Db.SingleOrDefaultAsync(i => i.InformationName == name);
        }
    }
}
