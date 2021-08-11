using Ant.Todos.Api.Database.Models;
using AutoMapper;

namespace Ant.Todos.Api.Features.Todos
{
    public class TodoMapper : Profile
    {
        public TodoMapper()
        {
            CreateMap<Todo, TodoViewModel>();
            CreateMap<UpdateTodo.Command, Todo>();
            CreateMap<CreateTodo.Command, Todo>();
        }
    }
}