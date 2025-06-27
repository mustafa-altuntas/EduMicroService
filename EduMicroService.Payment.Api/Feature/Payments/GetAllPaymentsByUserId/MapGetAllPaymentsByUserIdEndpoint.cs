using EduMicroService.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduMicroService.Payment.Api.Feature.Payments.GetAllPaymentsByUserId
{
    public static class MapGetAllPaymentsByUserIdEndpoint
    {

        public static RouteGroupBuilder MapGetAllPaymentsByUserIdEndpointExt(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllPaymentsByUserIdQuery());
                return result.ToGenericResult();
            })
            .MapToApiVersion(1, 0)
            .WithName("get-all-payments-by-userId")
            .Produces<List<GetAllPaymentsByUserIdResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            ;
            return group;
        }
    }
}
