namespace Ecommerce.Application.UseCases.Users.Commands.RegisterUser;

using Ecommerce.Application.UseCases.Users.Common;
using FluentResults;
using MediatR;

public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, Result<AuthenticationResult>>
{
    // Repository Injection

    // TokenGenerator INjection

    public Task<Result<AuthenticationResult>> Handle(
        RegisterUserCommand command,
        CancellationToken cancellationToken
    )
    {
        // 1. Check if the User Exists


        // 2. Create the User
        Console.WriteLine($"Text");

        // 3. Persist to the Database


        // 4. Generate the Token


        // 5. Return the Authentication Result
        return Task.FromResult<Result<AuthenticationResult>>(
            Result.Ok(
                new AuthenticationResult
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Email = command.Email,
                    Token = "token",
                }
            )
        );
    }
}
