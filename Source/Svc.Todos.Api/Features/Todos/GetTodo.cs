using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Ant.Platform.Exceptions;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Svc.Todos.Api.Database;
using Svc.Todos.Api.Database.Configurations;

namespace Svc.Todos.Api.Features.Todos
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
            public TodoViewModel Todo { get; set; }
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

                var mapped = _mapper.Map<TodoViewModel>(todo);
                var result = new Result { Todo = mapped };

                return result;
            }
        }
    }
}