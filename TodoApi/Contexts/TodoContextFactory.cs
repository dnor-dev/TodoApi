using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TodoApi.Contexts
{
    public class TodoContextFactory: IDesignTimeDbContextFactory<TodoContext>
    {
        public TodoContext CreateDbContext(string[] args) {
            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=authroles;Trusted_Connection=true");

            return new TodoContext(optionsBuilder.Options);
        }
    }
}
