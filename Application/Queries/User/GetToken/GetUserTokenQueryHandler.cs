﻿using Application.Queries.Users.GetToken;
using Infrastructure.Repositories.Users;
using MediatR;

namespace Application.Queries.User.GetToken
{
    public class GetUserTokenQueryHandler : IRequestHandler<GetUserTokenQuery, string>
    {
        private readonly IUserRepository _userRepository;

        public GetUserTokenQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(GetUserTokenQuery request, CancellationToken cancellationToken)
        {

            string token = await _userRepository.SignInUserByUsernameAndPassword(request.Username, request.Password);

            if (token == null)
            {
                return null!;
            }

            return token;

        }
    }
}