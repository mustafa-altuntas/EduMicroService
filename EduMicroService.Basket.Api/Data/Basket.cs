using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace EduMicroService.Basket.Api.Data
{
    // Anamic model den rich domanin modele geçiyoruz (entity/data + behavior)
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();

        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        [JsonIgnore] public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);

        [JsonIgnore] public decimal TotalPrice => BasketItems.Sum(x => x.Price);

        [JsonIgnore] public decimal? TotalPriceWithApplyDiscount => !IsApplyDiscount ? null : BasketItems.Sum(x => x.PriceByApplyDiscountRate);


        public Basket(Guid userId, List<BasketItem> basketItems)
        {
            UserId = userId;
            BasketItems = basketItems;
        }


        public void ApplyNewDiscount(string coupon, float discountRate)
        {
            Coupon = coupon;
            DiscountRate = discountRate;

            BasketItems.ForEach(item =>
            {
                item.PriceByApplyDiscountRate = item.Price * (decimal)(1 - discountRate);
            });
        }

        public void ApplyAvailableDiscountRate()
        {
            if(!IsApplyDiscount) return;

            BasketItems.ForEach(item =>
            {
                item.PriceByApplyDiscountRate = item.Price * (decimal)(1 - DiscountRate!);
            });
        }

        public void ClearDiscountRate()
        {
            DiscountRate = null;
            Coupon = null;
            BasketItems.ForEach(item =>
            {
                item.PriceByApplyDiscountRate = null;
            });
        }


    }
}
