using EduMicroService.Shared.Extensions;
using EduMicroService.Shared.Filters;
using MediatR;

namespace EduMicroService.Catalog.Api.Features.Categories.Create
{
    public static class CreateCategoryEndpointExt
    {
        public static RouteGroupBuilder CreateCategoryEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCategoryCommand command, IMediator mediator)
                => (await mediator.Send(command)).ToGenericResult())
                .WithName("CreateCategory")
                .AddEndpointFilter<ValidationFilter<CreateCategoryCommand>>();

            return group;
        }
    }
}
 