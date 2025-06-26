using EduMicroService.Shared.Filters;
using EduMicroService.Shared.Extensions;
using FluentValidation;
using MediatR;
using EduMicroService.Identity.Api.Dto;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Identity.Api.Features.Users.GetUserById;

public static class GetUserByIdEndpointExt
{
    public static RouteGroupBuilder MapGetUserByIdEndpointExt(this RouteGroupBuilder group)
    {
        group.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
        {
            var result = await mediator.Send(new GetUserByIdQuery(id));
            return result.ToGenericResult();
        })
        .WithName("GetUserById")
        .MapToApiVersion(1, 0)
        .Produces<UserDto>(StatusCodes.Status200OK)
        .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
        .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
        .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            ;

        return group;
    }
}

