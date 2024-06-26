using Ast.Users.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ast.Users.Features.Users;

internal class DeleteUser
{
    internal class Command : IRequest<Result>
    {
        public required Guid Id { get; set; }
    }

    internal class Result
    {
    }

    internal class Handler : IRequestHandler<Command, Result>
    {
        private readonly UserContext _context;

        public Handler(UserContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(Command request, CancellationToken ct)
        {
            await _context.Users
                .Where(u => u.Id == request.Id)
                .ExecuteDeleteAsync(ct);

            return new Result();
        }
    }
}
