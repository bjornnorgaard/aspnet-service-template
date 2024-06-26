using Ast.Users.Database;
using MediatR;

namespace Ast.Users.Features.Users;

internal class CreateUser
{
    internal class Command : IRequest<Result>
    {
        public required string Name { get; set; }
    }

    internal class Result
    {
        public required UserDto User { get; set; }
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
            var user = request.MapToUser();

            await _context.Users.AddAsync(user, ct);
            await _context.SaveChangesAsync(ct);

            return new Result { User = user.MapToDto() };
        }
    }
}
