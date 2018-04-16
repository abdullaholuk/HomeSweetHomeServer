using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;
using HomeSweetHomeServer.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HomeSweetHomeServer.Repositories
{
    public class FriendshipRepository : BaseRepository<FriendshipModel>, IFriendshipRepository
    {
        public FriendshipRepository(DatabaseContext context) : base(context)
        {
        }
    }
}
