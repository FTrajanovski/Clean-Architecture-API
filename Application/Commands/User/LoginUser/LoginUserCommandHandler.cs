using Application.Dtos;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Commands.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDto>
    {
        private readonly RealDatabase _dbContext;

        public LoginUserCommandHandler(RealDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loginUser = request.LoginUser;

            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserName == loginUser.UserName && u.Password == loginUser.Password);

            if (user != null)
            {
                return new UserDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    IsAdmin = user.IsAdmin
                };
            }

            return null;
        }
    }
}
