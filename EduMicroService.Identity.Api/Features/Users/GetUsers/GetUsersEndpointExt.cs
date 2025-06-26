using FluentValidation;
using MediatR;
using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.GetUsers;

public static class GetUsersEndpointExt
{
    public static RouteGroupBuilder MapGetUsersEndpointExt(this RouteGroupBuilder group)
    {
        group.MapGet("/", async ([FromServices]IMediator mediator) =>
        {
            var result = await mediator.Send(new GetUsersQuery());
            return result.ToGenericResult();
        })
        .WithName("GetUsers")
        .MapToApiVersion(1, 0)
        .Produces<List<UserDto>>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            ;

        return group;
    }
}

