using TodoApi.Models;

namespace TodoApi.Interfaces
{
    public interface ITodoService
    {
        Task<Todo> CreateTodoAsync(Todo model);

        Task<Todo> UpdateTodoAsync(Guid Id, Todo model);

        Task<Todo> GetTodoAsync(Guid Id);

        Task DeleteTodoAsync(Guid Id);

        Task<List<Todo>> GetTodos();

    }
}
