using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Catalog.Api.Features.Courses.Update
{
    public static class UpdateCourseEndpoint
    {
        public static RouteGroupBuilder UpdateCourseEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateCourseCommand command, IMediator mediator)
                => (await mediator.Send(command)).ToGenericResult())
                .WithName("UpdateCourse")
                .MapToApiVersion(1, 0)
                .Produces(StatusCodes.Status200OK)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();

            return group;
        }
    }
}
