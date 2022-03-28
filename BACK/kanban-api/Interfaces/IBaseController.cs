using Microsoft.AspNetCore.Mvc;

namespace kanban_api.Interfaces
{
    public interface IBaseController<T>
    {
        Task<IQueryable<T>> Get();

        Task<IActionResult> Post(T entity);

        Task<IActionResult> Put(Guid key, T entity);

        Task<IActionResult> Delete(Guid key);
    }
}
