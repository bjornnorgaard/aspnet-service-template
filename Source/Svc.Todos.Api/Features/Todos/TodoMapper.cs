using AutoMapper;
using Svc.Todos.Api.Database.Models;

namespace Svc.Todos.Api.Features.Todos
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