using System;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Ant.Todo.Api.Database;
using Ant.Todo.Api.Database.Configurations;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ant.Todo.Api.Features.Todos
{
    public class CreateTodo
    {
        public class Command : IRequest<Result>
        {
            public string Title { get; set; }
            public string Description { get; set; }
            [JsonIgnore] public string UserId { get; set; }
        }

        public class Result
        {
            public Guid TodoId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Title).NotEmpty()
                    .MinimumLength(TodoConstants.Title.MinLength)
                    .MaximumLength(TodoConstants.Title.MaxLength);

                RuleFor(c => c.Description)
                    .MaximumLength(TodoConstants.Description.MaxLength);

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
                var todo = _mapper.Map<Database.Models.Todo>(request);

                await _todoContext.Todos.AddAsync(todo, ct);
                await _todoContext.SaveChangesAsync(ct);

                var result = new Result { TodoId = todo.Id };
                return result;
            }
        }
    }
}