using Asp.Versioning.Builder;

namespace EduMicroService.Order.Api.Endpoints.Orders
{
    public static class OrderEndpointExt
    {
        public static void AddOrderEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/Orders")
                .WithTags("Orders")
                .WithApiVersionSet(apiVersionSet)
                .CreateOrderEndpointExt()
                .GetOrdersByUserIdEndpointExt()
            ;

        }
    }
}
