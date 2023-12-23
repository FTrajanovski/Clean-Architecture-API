using Application.Dtos;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
    {
        private readonly RealDatabase _dbContext;

        public UpdateUserCommandHandler(RealDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var updatedUser = await _dbContext.Users.FindAsync(request.UserId);

            if (updatedUser != null)
            {
                // Uppdatera användarens egenskaper
                updatedUser.UserName = request.UpdatedUser.UserName;
                updatedUser.Password = request.UpdatedUser.Password;
                updatedUser.IsAdmin = request.UpdatedUser.IsAdmin;

                await _dbContext.SaveChangesAsync(cancellationToken);

                return new UserDto
                {
                    UserId = updatedUser.UserId,
                    UserName = updatedUser.UserName,
                    IsAdmin = updatedUser.IsAdmin
                };
            }

            return null;
        }
    }
}
