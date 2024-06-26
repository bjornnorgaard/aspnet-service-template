using Ast.Platform.Exceptions;
using Ast.Users.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ast.Users.Features.Users;

internal class GetUser
{
    internal class Command : IRequest<Result>
    {
        public required Guid Id { get; set; }
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
            var found = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, ct);
            if (found is null) throw new PlatformException(PlatformError.UserNotFound);
            return new Result { User = found.MapToDto() };
        }
    }
}
