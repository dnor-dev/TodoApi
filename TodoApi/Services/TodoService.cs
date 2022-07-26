using TodoApi.Interfaces;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository repository;

        public TodoService(ITodoRepository todoRepository)
        {
            repository = todoRepository;
        }
        public async Task<Todo> CreateTodoAsync(Todo model)
        {
           if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            var entity = new Todo
            {
                Description = model.Description,
                IsCompleted = model.IsCompleted
            };
            entity = await repository.AddAsync(entity);

            return new Todo
            {
                Id = entity.Id,
                Description = entity.Description,
                IsCompleted = entity.IsCompleted
            };
        }

        public async Task DeleteTodoAsync(Guid Id)
        {
            await repository.RemoveAsync(Id);
        }

        public async Task<Todo> GetTodoAsync(Guid Id)
        {
            var entity = await repository.FindAsync(Id);

            if (entity is null)
            {
                return null;
            }

            return new Todo
            {
                Id = entity.Id,
                IsCompleted = entity.IsCompleted,
                Description = entity.Description
            };

        }

        public async Task<List<Todo>> GetTodos()
        {
            IQueryable<Todo> allEntities = repository.GetAllAsync();
            return await allEntities.Select(todo => new Todo
            {
                Id = todo.Id,
                IsCompleted = todo.IsCompleted,
                Description = todo.Description
            }).ToListAsync();
        }

        public async Task<Todo> UpdateTodoAsync(Guid Id, Todo model)
        {
            var entity = new Todo
            {
                Id = model.Id,
                IsCompleted = model.IsCompleted,
                Description = model.Description
            };

            entity = await repository.UpdateAsync(Id,entity);
            return entity;
        }
    }
}
