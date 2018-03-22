using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeSweetHomeServer.Repositories
{
    //Base repository operations
    public interface IRepository<TEntity> where TEntity : class 
    {
        void addUser(TEntity user);
    }
}
