using System.Text.Json.Serialization;

namespace EduMicroService.Basket.Api.Dto
{
    public record BasketDto
    {
        [JsonIgnore] public bool IsApplyDiscount => DiscountRate is > 0 && !string.IsNullOrEmpty(Coupon);

        public List<BasketItemDto> BasketItems { get; set; } = new();

        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }


        public decimal TotalPrice => BasketItems.Sum(x => x.Price);

        public decimal? TotalPriceWithApplyDiscount => !IsApplyDiscount ? null : BasketItems.Sum(x => x.PriceByApplyDiscountRate);





        public BasketDto( List<BasketItemDto> items)
        {
            BasketItems = items;
        }

        public BasketDto()
        {
        }


    }
}
