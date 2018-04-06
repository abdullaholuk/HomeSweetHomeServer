namespace HomeSweetHomeServer.Repositories
{
    //Base repository operations
    public interface IRepository<TEntity> where TEntity : class 
    {
        void Add(TEntity entity);
    }
}
