using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    public interface IUserInformationRepository : IBaseRepository<UserInformationModel>
    {
        Task<UserInformationModel> GetUserInformationByValueAsync(string value);
        Task<List<UserInformationModel>> GetAllUserInformationsByUserIdAsync(int userId);
        Task<UserInformationModel> GetUserInformationByIdAsync(int userId, int informationId);
    }
}
