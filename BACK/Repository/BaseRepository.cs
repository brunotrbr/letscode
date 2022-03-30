using kanban_api.Context;
using kanban_api.Interfaces;

namespace kanban_api.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly KanbanContext _context;

        public BaseRepository(KanbanContext kanbanContext)
        {
            _context = kanbanContext;
        }

        public Task<Guid> Delete(Guid key)
        {
            return Task.Run(() =>
            {
                var entity = _context.Find<T>(key);

                if (entity == null)
                {
                    throw new Exception("ID inexistente.");
                }

                _context.Remove(entity);
                _context.SaveChanges();
                return key;
            });
        }

        public Task<IQueryable<T>> Get()
        {
            return Task.Run(() =>
            {
                var data = _context.Set<T>().AsQueryable();
                return data.Any() ? data : new List<T>().AsQueryable();
            });
        }

        public Task<T?> GetByKey(Guid key)
        {
            return Task.Run(() =>
            {
                return _context.Find<T>(key);
            });
        }

        public Task<T> Insert(T entity)
        {
            return Task.Run(() =>
            {
                _context.Add(entity);
                _context.SaveChanges();
                return entity;
            });
        }

        public Task<T> Update(Guid key, T entity)
        {
            return Task.Run(() =>
            {
                _context.Update(entity);
                _context.SaveChanges();
                return entity;
            });
        }
    }
}
