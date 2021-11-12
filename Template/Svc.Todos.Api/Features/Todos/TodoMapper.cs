using AutoMapper;
using Svc.Todos.Api.Database.Models;

namespace Svc.Todos.Api.Features.Todos
{
    public class TodoMapper : Profile
    {
        public TodoMapper()
        {
            CreateMap<Todo, TodoDto>();

            CreateMap<UpdateTodo.Command, Todo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CreateTodo.Command, Todo>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsCompleted, opt => opt.Ignore());
        }
    }
}