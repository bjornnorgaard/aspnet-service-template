using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ant.Todo.Api.Controllers.Requests.Todos;
using Ant.Todo.Api.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ant.Todo.Api.Controllers
{
    [ApiController]
    [Route("todos")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Database.Models.Todo>>> GetTodos(
            [FromQuery] int take = 10,
            [FromQuery] int skip = 0)
        {
            var todos = await _context.Todos.AsNoTracking()
                .OrderBy(t => t.Id)
                .Skip(skip).Take(take)
                .ToListAsync();

            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Database.Models.Todo>> GetTodo(
            [FromRoute] int id)
        {
            var todo = await _context.Todos.AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null) return NotFound();

            return Ok(todo);
        }

        [HttpPost("")]
        public async Task<ActionResult<int>> CreateTodo(
            [FromBody] CreateTodoRequest request)
        {
            var todo = new Database.Models.Todo
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = false
            };

            await _context.Todos.AddAsync(todo);
            await _context.SaveChangesAsync();

            return Ok(todo.Id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> UpdateTodo(
            [FromRoute] int id,
            [FromBody] UpdateTodoRequest request)
        {
            var todo = await _context.Todos.AsNoTracking()
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (todo == null) return NotFound();

            todo.Title = request.Title;
            todo.Description = request.Description;
            todo.IsCompleted = request.IsCompleted;

            await _context.SaveChangesAsync();

            return Ok(todo.Id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteTodo(
            [FromRoute] int id)
        {
            var todo = await _context.Todos.AsNoTracking()
                .Where(t => t.Id == id)
                .FirstOrDefaultAsync();

            if (todo == null) return NotFound();

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}