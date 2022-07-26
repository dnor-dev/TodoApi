using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo> FindAsync(Guid Id);

        Task<Todo> UpdateAsync(Guid Id,Todo Todo);

        Task<Todo> AddAsync(Todo Todo);

        Task RemoveAsync(Guid Id);

        IQueryable<Todo> GetAllAsync();
    }
}
