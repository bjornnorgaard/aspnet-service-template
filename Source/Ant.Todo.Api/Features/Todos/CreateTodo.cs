using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Ant.Todo.Api.Database;
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
        }
        
        public class Result
        {
            public Guid TodoId { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Title).NotEmpty().MinimumLength(2).MaximumLength(25);
                RuleFor(c => c.Description).MaximumLength(100);
            }
        }
        
        public class Handler : IRequestHandler<Command, Result>
        {
            public Context Context { get; set; }
            
            public Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}