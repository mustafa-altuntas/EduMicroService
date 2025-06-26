using MediatR;
using EduMicroService.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Payment.Api.Feature.Payments.Create
{
    public static class MapCreatePaymentEndpoint
    {

        public static RouteGroupBuilder MapCreatePaymentEndpointExt(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreatePaymentCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            })
            .MapToApiVersion(1, 0)
            .WithName("Create")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            ;
            return group;
        }
    }
}
