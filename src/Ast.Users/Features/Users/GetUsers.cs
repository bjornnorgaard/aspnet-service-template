using Ast.Users.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ast.Users.Features.Users;

internal class GetUsers
{
    internal class Command : IRequest<Result>
    {
        public required int Take { get; set; }
        public required int Skip { get; set; }
    }

    internal class Result
    {
        public required List<UserDto> Users { get; set; }
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
            var users = await _context.Users
                .Skip(request.Skip)
                .Take(request.Take)
                .Select(u => u.MapToDto())
                .ToListAsync(ct);

            return new Result { Users = users };
        }
    }
}
