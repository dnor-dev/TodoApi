using TodoApi.Contexts;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Repository
{
    public class TodoRepository: ITodoRepository
    {
        private readonly TodoContext todoDB;

        public TodoRepository(TodoContext _todoDB)
        {
            todoDB = _todoDB;
        }

        public async Task<Todo> AddAsync (Todo todo)
        {
            todo.Id = todo.Id == Guid.Empty ? Guid.NewGuid() : todo.Id;
            await todoDB.Todos.AddAsync (todo);
            await todoDB.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> FindAsync(Guid Id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await todoDB.Todos.FindAsync(Id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public IQueryable<Todo> GetAllAsync()
        {
            return todoDB.Todos.AsQueryable();
        }

        public async Task RemoveAsync(Guid Id)
        {
            var todo = await todoDB.Todos.FindAsync(Id);

            if (todo is not null)
            {
                todoDB.Todos.Remove(todo);
                await todoDB.SaveChangesAsync();
            }
        }

        public async Task<Todo> UpdateAsync(Guid Id, Todo todo)
        {
            var oldTodo = await this.FindAsync(Id);
            Console.WriteLine(oldTodo);
            todoDB.Entry(oldTodo).CurrentValues.SetValues(todo);
            await todoDB.SaveChangesAsync();
            return todo; 
        }
    }
}
