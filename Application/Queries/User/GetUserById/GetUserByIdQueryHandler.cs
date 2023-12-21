using Application.Dtos;
using Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Queries.User.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly RealDatabase _dbContext;

        public GetUserByIdQueryHandler(RealDatabase dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.UserId == request.UserId);

            if (user != null)
            {
                return new UserDto
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Password = user.Password,
                    IsAdmin = user.IsAdmin
                };
            }

            return null;
        }
    }
}