using EduMicroService.Payment.Api.Repositories;

namespace EduMicroService.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;

public record GetAllPaymentsByUserIdResponse(
    Guid Id,
    string OrderCode,
    string Amount,
    DateTime Created,
    PaymentStatus Status);