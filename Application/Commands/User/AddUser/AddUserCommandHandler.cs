using Application.Dtos;
using Infrastructure.Database;
using MediatR;

namespace Application.Commands.User.AddUser
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDto>
    {
        private readonly RealDatabase _dbContext;
        private MockDatabase mockDatabase;

        public AddUserCommandHandler(RealDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        public AddUserCommandHandler(MockDatabase mockDatabase)
        {
            this.mockDatabase = mockDatabase;
        }

        public async Task<UserDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var newUser = new Domain.Models.User
            {
                UserName = request.NewUser.UserName,
                Password = request.NewUser.Password,
                IsAdmin = request.NewUser.IsAdmin
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new UserDto
            {
                UserId = newUser.UserId,
                UserName = newUser.UserName,
                IsAdmin = newUser.IsAdmin
            };
        }
    }
}
