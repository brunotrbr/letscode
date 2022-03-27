namespace kanban_api.Interface
{
    public interface IBaseRepository<TEntity>
    {
        IQueryable<TEntity> Get();

        TEntity GetByKey(Guid key);

        void Insert(TEntity entity);

        void Update(TEntity entity);

        void Delete(Guid key);
    }
}
