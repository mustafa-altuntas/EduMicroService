using MassTransit;

namespace EduMicroService.Order.Domain.Entities
{
    public class Order : BaseEntity<Guid>
    {
        public string Code { get; set; } = null!;

        public DateTime Created { get; set; }

        public Guid BuyerId { get; set; }

        public OrderStatus Status { get; set; }

        public int AddressId { get; set; }

        public decimal TotalPrice { get; set; }

        public float? DiscountRate { get; set; }

        public Guid? PaymentId { get; set; }


        public List<OrderItem> OrderItems { get; set; } = new();

        public Address Address { get; set; } = null!;


        public static string GenerateCode(int length = 10)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray());
        }


        public static Order CreateUnPaidOrder(Guid buyerId, float? disCountRate, int addressId)
        {
            return new Order
            {
                Id = NewId.NextGuid(),
                Code = GenerateCode(),
                BuyerId = buyerId,
                Created = DateTime.Now,
                Status = OrderStatus.WaitingForPayment,
                AddressId = addressId,
                DiscountRate = disCountRate,
                TotalPrice = 0
            };
        }

        public static Order CreateUnPaidOrder(Guid buyerId, float? disCountRate)
        {
            return new Order
            {
                Id = NewId.NextGuid(),
                Code = GenerateCode(),
                BuyerId = buyerId,
                Created = DateTime.Now,
                Status = OrderStatus.WaitingForPayment,
                DiscountRate = disCountRate,
                TotalPrice = 0
            };
        }

        public void AddOrderItem(Guid productId, string productName, decimal unitPrice)
        {
            var orderItem = new OrderItem();


            if (DiscountRate.HasValue) unitPrice -= unitPrice * (decimal)DiscountRate.Value / 100;


            orderItem.SetItem(productId, productName, unitPrice);
            OrderItems.Add(orderItem);

            CalculateTotalPrice();
        }


        public void ApplyDiscount(float discountPercentage)
        {
            if (discountPercentage < 0 || discountPercentage > 100) throw new ArgumentNullException("Discount percentage must be between 0 and 100");
            DiscountRate = discountPercentage;
            CalculateTotalPrice();
        }

        public void SetPaidStatus(Guid paymentId)
        {
            Status = OrderStatus.Paid;
            PaymentId = paymentId;
        }

        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(x => x.UnitPrice);
        }
    }
}