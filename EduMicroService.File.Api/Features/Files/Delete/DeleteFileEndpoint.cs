using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.File.Api.Features.Files.Delete
{
    public static class DeleteFileEndpoint
    {
        public static RouteGroupBuilder DeleteFileEndpointExt(this RouteGroupBuilder group)
        {
            group.MapDelete("",
                    async ([FromBody] DeleteFileCommand deleteFileCommand, IMediator mediator) =>
                    (await mediator.Send(deleteFileCommand)).ToGenericResult())
                .WithName("delete")
                .MapToApiVersion(1, 0)
                .Produces<Guid>(StatusCodes.Status204NoContent)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

            return group;
        }
    }
}
