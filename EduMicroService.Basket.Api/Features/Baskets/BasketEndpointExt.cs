using Asp.Versioning.Builder;
using EduMicroService.Basket.Api.Features.Baskets.AddBasketItem;
using EduMicroService.Basket.Api.Features.Baskets.ApplyDiscountCoupon;
using EduMicroService.Basket.Api.Features.Baskets.DeleteBasketItem;
using EduMicroService.Basket.Api.Features.Baskets.GetBasket;
using EduMicroService.Basket.Api.Features.Baskets.RemoveDiscountCoupon;

namespace EduMicroService.Basket.Api.Features.Baskets
{
    public static class BasketEndpointExt
    {
        public static void AddBasketEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/baskets")
                .WithTags("Baskets")
                .WithApiVersionSet(apiVersionSet)
                .AddBasketItemEndpointExt()
                .DeleteBasketItemEndpointExt()
                .GetBasketEndpointExt()
                .ApplyDiscountCouponEndpointExt()
                .RemoveDiscountCouponEndpointExt()
                ;
        }

    }
}
