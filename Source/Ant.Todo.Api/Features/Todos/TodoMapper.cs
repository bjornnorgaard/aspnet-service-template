using AutoMapper;

namespace Ant.Todo.Api.Features.Todos
{
    public class TodoMapper : Profile
    {
        public TodoMapper()
        {
            CreateMap<Database.Models.Todo, TodoViewModel>();
            CreateMap<UpdateTodo.Command, Database.Models.Todo>();
            CreateMap<CreateTodo.Command, Database.Models.Todo>();
        }
    }
}