using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Mvc;
using TtaApp.Todo.Application.Todos.Commands;
using TtaApp.Todo.Application.Todos.Queries;
using TtaApp.Todo.Application.Todos.Queries.DTOs;

namespace TtaApp.Todo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public TodoController(
            ICommandDispatcher commandDispatcher, 
            IQueryDispatcher queryDispatcher
        )
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<IEnumerable<TodoDTO>> GetList()
        {
            return await _queryDispatcher.QueryAsync<GetTodos, IEnumerable<TodoDTO>>(new GetTodos());
        }

        [HttpPost("[action]")]
        public async Task Add(
            AddTodo command
        )
        {
            await _commandDispatcher.SendAsync(command);
        }

        [HttpPost("[action]")]
        public async Task ChangeStatus(
            ChangeTodoStatus command
        )
        {
            await _commandDispatcher.SendAsync(command);
        }

        [HttpPost("[action]")]
        public async Task Delete(
            DeleteTodo command
        )
        {
            await _commandDispatcher.SendAsync(command);
        }
    }
}
