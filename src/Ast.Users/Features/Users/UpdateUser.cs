using Ast.Platform.Exceptions;
using Ast.Users.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ast.Users.Features.Users;

internal class UpdateUser
{
    internal class Command : IRequest<Result>
    {
        public required Guid Id { get; set; }
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.Id, ct);
            if (user is null) throw new PlatformException(PlatformError.UserNotFound);

            user = request.MapToUser();
            await _context.SaveChangesAsync(ct);

            return new Result { User = user.MapToDto() };
        }
    }
}
