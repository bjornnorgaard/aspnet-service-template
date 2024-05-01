using Api.Todos.Database.Models;
using AutoMapper;

namespace Api.Todos.Features.Todos;

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