using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    public interface IFriendshipRepository : IBaseRepository<FriendshipModel>
    {
        Task<FriendshipModel> GetFriendshipByIdAsync(int user1Id, int user2Id, bool include = false);
    }
}