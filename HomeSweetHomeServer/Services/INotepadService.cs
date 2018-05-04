using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Services
{
    //Interface about notepad operations
    public interface INotepadService
    {
        Task<List<SynchronizeModel<NotepadModel>>> SynchronizeNotepad(UserModel user, ClientNotepadContextModel client);
        Task AddNote(UserModel user, NotepadModel note);
        Task DeleteNote(UserModel user, int noteId);
        Task UpdateNote(UserModel user, NotepadModel note);
    }
}
