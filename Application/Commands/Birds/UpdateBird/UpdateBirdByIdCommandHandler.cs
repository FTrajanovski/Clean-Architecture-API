﻿using Domain.Models;
using Infrastructure.Database;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Birds.UpdateBird
{
    public class UpdateBirdByIdCommandHandler : IRequestHandler<UpdateBirdByIdCommand, Bird>
    {
        private readonly RealDatabase _realDatabase;

        public UpdateBirdByIdCommandHandler(RealDatabase realDatabase)
        {
            _realDatabase = realDatabase;
        }
        public Task<Bird> Handle(UpdateBirdByIdCommand request, CancellationToken cancellationToken)
        {
            Bird birdToUpdate = _realDatabase.Birds.FirstOrDefault(bird => bird.Id == request.Id)!;

            birdToUpdate.Name = request.UpdatedBird.Name;
            birdToUpdate.CanFly = request.UpdatedBird.CanFly;


            return Task.FromResult(birdToUpdate);
        }
    }

}