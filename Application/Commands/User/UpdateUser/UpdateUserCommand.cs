using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserDto>
    {
        public UserDto UpdatedUser { get; }
        public Guid UserId { get; }

        public UpdateUserCommand(UserDto updatedUser, Guid userId)
        {
            UpdatedUser = updatedUser;
            UserId = userId;
        }
    }

}
