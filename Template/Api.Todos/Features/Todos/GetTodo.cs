using System.Text.Json.Serialization;
using Ant.Platform.Exceptions;
using Api.Todos.Database;
using Api.Todos.Database.Configurations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Todos.Features.Todos
{
    public class GetTodo
    {
        public class Command : IRequest<Result>
        {
            public Guid TodoId { get; set; }
            [JsonIgnore] public string UserId { get; set; }
        }

        public class Result
        {
            public TodoDto Todo { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.TodoId).NotEmpty();

                RuleFor(c => c.UserId).NotEmpty()
                    .MinimumLength(TodoConstants.UserId.MinLenght)
                    .MaximumLength(TodoConstants.UserId.MaxLength);
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly TodoContext _todoContext;
            private readonly IMapper _mapper;

            public Handler(TodoContext todoContext, IMapper mapper)
            {
                _todoContext = todoContext;
                _mapper = mapper;
            }

            public async Task<Result> Handle(Command request, CancellationToken ct)
            {
                var todo = await _todoContext.Todos.AsNoTracking()
                    .Where(t => t.Id == request.TodoId)
                    .Where(t => t.UserId == request.UserId)
                    .FirstOrDefaultAsync(ct);

                if (todo == null) throw new PlatformException(PlatformError.TodoNotFound);

                var mapped = _mapper.Map<TodoDto>(todo);
                var result = new Result { Todo = mapped };

                return result;
            }
        }
    }
}
