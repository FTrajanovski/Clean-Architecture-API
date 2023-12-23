using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User.LoginUser
{
    public class LoginUserCommand : IRequest<UserDto>
    {
        public UserDto LoginUser { get; }

        public LoginUserCommand(UserDto loginUser)
        {
            LoginUser = loginUser;
        }
    }
}
