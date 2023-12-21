using Infrastructure.Database;
using MediatR;

namespace Application.Commands.User.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly RealDatabase _dbContext;

        public DeleteUserCommandHandler(RealDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userToDelete = await _dbContext.Users.FindAsync(request.UserId);

            if (userToDelete != null)
            {
                _dbContext.Users.Remove(userToDelete);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return true;
            }

            return false;
        }
    }
}
