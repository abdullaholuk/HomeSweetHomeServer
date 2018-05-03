using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HomeSweetHomeServer.Repositories
{
    public class NotepadRepository : BaseRepository<NotepadModel>, INotepadRepository
    {
        public NotepadRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<List<NotepadModel>> GetAllNoteByHomeId(int homeId, bool include = false)
        {
            if (include == false)
                return await Db.Where(n => n.Home.Id == homeId).ToListAsync();
            else
                return await Db.Where(n => n.Home.Id == homeId).Include(n => n.Home).ThenInclude(h => h.Users).ToListAsync(); 
        }

        public async Task<NotepadModel> GetNoteById(int id, bool include = false)
        {
            if (include == false)
                return await Db.SingleOrDefaultAsync(n => n.Id == id);
            else
                return await Db.Include(n => n.Home).ThenInclude(h => h.Users).SingleOrDefaultAsync(n => n.Id == id);
        }

    }
}
