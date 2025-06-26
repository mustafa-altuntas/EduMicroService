using Asp.Versioning.Builder;
using EduMicroService.Payment.Api.Feature.Payments.Create;

namespace EduMicroService.Payment.Api.Feature.Payments
{
    public static class PaymentEndpointExt
    {
        public static void AddPaymentEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("/api/v{version:apiVersion}/payments")
                .WithTags("Payments")
                .WithApiVersionSet(apiVersionSet)
                .MapCreatePaymentEndpointExt()
                ;
 
        }
    }
}
