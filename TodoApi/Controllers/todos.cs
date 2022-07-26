using Microsoft.AspNetCore.Mvc;
using TodoApi.Interfaces;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class todos : ControllerBase
    {
        private readonly ITodoService todoService;

        public todos(ITodoService todoService)
        {
            this.todoService = todoService;
        }


        // GET: api/<todos>
        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos()
        {
            List<Todo> todos = await todoService.GetTodos();

            return Ok(todos);
        }

        // GET api/<todos>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid Id)
        {
            Todo todo = await todoService.GetTodoAsync(Id);

            if (todo is null)
            {
                return StatusCode(404, "Not found");
            }
            return Ok(todo);
        }

        // POST api/<todos>
        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo model)
        {
            Todo todo = new Todo
            {
                IsCompleted = model.IsCompleted,
                Description = model.Description
            };

            var createdTodo = await todoService.CreateTodoAsync(todo);

            return Ok(createdTodo);
        }

        // PUT api/<todos>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTodo(Guid Id, [FromBody] Todo model)
        {
            if (Id != model.Id)
            {
                return BadRequest();
            }

            Todo todo = await todoService.GetTodoAsync(Id);

            if (todo is null)
            {
                return NotFound();
            }

            await todoService.UpdateTodoAsync(Id,model);

            return StatusCode(200, "Todo Updated");
        }

        // DELETE api/<todos>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(Guid Id)
        {
            Todo todo = await todoService.GetTodoAsync(Id);

            if (todo is null)
            {
                return NotFound();
            }

            await todoService.DeleteTodoAsync(Id);

            return StatusCode(200, "Todo deleted!");
        }
    }
}
