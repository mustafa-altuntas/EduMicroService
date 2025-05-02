    using Asp.Versioning.Builder;
using EduMicroService.Discount.Api.Features.Discounts.CreateDiscount;
using EduMicroService.Discount.Api.Features.Discounts.GetDiscountByCode;

namespace EduMicroService.Discount.Api.Features.Discounts
{
    public static class DiscountEndpointExt
    {
        public static void AddDiscountEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/discounts")
                .WithTags("Discounts")
                .WithApiVersionSet(apiVersionSet)
                .CreateDiscountEndpointExt()
                .GetDiscountByCodeEndpointExt()
            ;

        }
    }
}
