using Domain.Models;
using Infrastructure.Database.Repositories.UserRepo;
using MediatR;
using Org.BouncyCastle.Crypto.Generators;

namespace Application;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, User>
{

    private readonly IUserRepository _userRepository;

    public AddUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        User userToCreate = new()
        {
            Id = Guid.NewGuid(),
            UserName = request.NewUser.UserName,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewUser.Password),

        };

        await _userRepository.AddUserAsync(userToCreate);

        return userToCreate;




    }
}