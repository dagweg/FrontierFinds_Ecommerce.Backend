using Ecommerce.Application.UnitTests.UseCases.Users.TestUtils;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;

public static class LoginUserQueryUtils
{
    public static LoginUserQuery CreateQuery()
    {
        return new LoginUserQuery(Constants.User.Email, Constants.User.Password);
    }
}
