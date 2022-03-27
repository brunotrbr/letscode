using kanban_api.Context;
using kanban_api.Interface;


namespace kanban_api.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    {
        protected readonly KanbanApiContext Context;

        public BaseRepository(KanbanApiContext context)
        {
            Context = context;
        }

        public virtual IQueryable<TEntity> Get()
        {
            var data = Context.Set<TEntity>().AsQueryable();
            return data.Any() ? data : new List<TEntity>().AsQueryable();
        }

        public TEntity GetByKey(Guid key)
        {
            return Context.Set<TEntity>().Find(key);
        }

        public void Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Context.Set<TEntity>().Update(entity);
            Context.SaveChanges();
        }

        public void Delete(Guid key)
        {
            var entity = Context.Set<TEntity>().Find(key);

            if (entity != null)
                Context.Set<TEntity>().Remove(entity);
            else
                throw new Exception("ID inexistente.");
            Context.SaveChanges();
        }
    }
}
