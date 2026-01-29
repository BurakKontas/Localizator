using Localizator.Shared.Mediator.Interfaces;

namespace Localizator.User.Application;

public class GetUserHandler : IRequestHandler<CreateUserCommand, UserCreatedResponse>
{
    public async Task<UserCreatedResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw new TimeoutException();

        return new UserCreatedResponse()
        {
            Email = request.Email,
            Name = request.Name,
        };
    }
}

public class CreateUserCommand : IRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class UserCreatedResponse : IResponse
{
    public string Name { get; set; }
    public string Email { get; set; }
}