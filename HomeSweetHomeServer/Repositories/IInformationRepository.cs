using System.Collections.Generic;
using System.Threading.Tasks;
using HomeSweetHomeServer.Models;

namespace HomeSweetHomeServer.Repositories
{
    public interface IInformationRepository : IBaseRepository<InformationModel>
    {
        Task<List<InformationModel>> GetAllAsync();
        Task<InformationModel> GetInformationByInformationNameAsync(string name);
    }
}
