using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    public interface INotepadRepository : IBaseRepository<NotepadModel>
    {
        Task<List<NotepadModel>> GetAllNoteByHomeId(int homeId, bool include = false);
        Task<NotepadModel> GetNoteById(int id, bool include = false);
    }
}
