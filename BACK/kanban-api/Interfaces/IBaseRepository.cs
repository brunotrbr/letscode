using Microsoft.AspNetCore.Mvc;

namespace kanban_api.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<IQueryable<T>> Get();

        Task<T?> GetByKey(Guid key);

        Task<T> Insert(T entity);

        Task<T> Update(Guid key, T entity);

        Task<Guid> Delete(Guid key);
    }
}
