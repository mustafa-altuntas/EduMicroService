using EduMicroService.Shared;

namespace EduMicroService.Payment.Api.Feature.Payments.GetAllPaymentsByUserId;
 
    public record GetAllPaymentsByUserIdQuery : IRequestByServiceResult<List<GetAllPaymentsByUserIdResponse>>;


