﻿
using FlightAgency.Application.Common;
using FlightAgency.Application.Features.Authorization.Requests;
using FlightAgency.Infrastructure;
using LanguageExt;

namespace FlightAgency.Application.Features.Authorization.AuthorizationHandler;

public interface IAuthorizationHandler
{
    Either<string, User> Login(LoginRequest loginRequest);
    Either<string, User> Register(CreateUserRequest createUserRequest);
}

public class AuthorizationHandler : IAuthorizationHandler
{
    public UserContext UserContext;
    public AuthorizationHandler(UserContext userContext)
    {
        UserContext = userContext;
    }

    public Either<string, User> Login(LoginRequest loginRequest) =>
        UserContext
            .Users
            .FindUserByEmail(loginRequest.Email)
            .Match<Either<string, User>>(None: () => "User does not exist.",
                                         Some: (user) => user)
            .Match<Either<string, User>>(Left: (error) => error,
                                         Right: (user) => user.Password == loginRequest.Password
                                                            ? user
                                                            : "Password does not match.");

    public Either<string, User> Register(CreateUserRequest request) =>
        UserContext
            .Users
            .FindUserByEmail(request.Email)
            .Match<Either<string, User>>(None: () => new User(request.Email, request.Password, request.Name),
                                         Some: (_) => "Already exists.")
            .Match<Either<string, User>>(Left: (error) => error,
                                         Right: (user) => PersistUser(user));

    private User PersistUser(User user)
    {
        UserContext.Users.Add(user);
        UserContext.SaveChanges();
        return user;
    }
}
