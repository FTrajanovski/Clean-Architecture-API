﻿using Domain.Models;
using MediatR;

namespace Application;

public class DeleteUserByIdCommand : IRequest<User>
{
    public DeleteUserByIdCommand(Guid id)
    {
        Guid Id = id;
    }

    public Guid Id { get; }

}