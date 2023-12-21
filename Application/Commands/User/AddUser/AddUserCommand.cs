using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User.AddUser
{
    public class AddUserCommand : IRequest<UserDto>
    {
        public UserDto NewUser { get; }

        public AddUserCommand(UserDto newUser)
        {
            NewUser = newUser;
        }
    }
}
