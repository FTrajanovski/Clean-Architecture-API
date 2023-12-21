using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.User.DeleteUser
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid UserId { get; }

        public DeleteUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}
