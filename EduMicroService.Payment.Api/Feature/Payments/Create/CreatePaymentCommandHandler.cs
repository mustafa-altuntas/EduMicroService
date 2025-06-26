﻿using EduMicroService.Payment.Api.Repositories;
using EduMicroService.Shared;
using EduMicroService.Shared.Services;
using MediatR;

namespace EduMicroService.Payment.Api.Feature.Payments.Create
{
    public class CreatePaymentCommandHandler(AppDbContext appDbContext, IIdentityService idenIdentityService)
        : IRequestHandler<CreatePaymentCommand, ServiceResult<Guid>>
    {


        public async Task<ServiceResult<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var (isSuccess, errorMessage) = await ExternalPaymentProcessAsync(request.CardNumber,
                request.CardHolderName,
                request.CardExpirationDate, request.CardSecurityNumber, request.Amount);


            if (!isSuccess)
            {
                return ServiceResult<Guid>.Error("Payment Failed", errorMessage!, System.Net.HttpStatusCode.BadRequest);
            }


            var userId = idenIdentityService.GetUserId;
            var newPayment = new Repositories.Payment(userId, request.OrderCode, request.Amount);
            newPayment.SetStatus(PaymentStatus.Success);

            appDbContext.Payments.Add(newPayment);
            await appDbContext.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(newPayment.Id,"<empty>");
        }


        private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber,
            string cardHolderName, string cardExpirationDate, string cardSecurityNumber, decimal amount)
        {
            // Simulate external payment processing logic
            await Task.Delay(1000); // Simulating a delay for the external service call
            return (true, null); // Assume the payment was successful

            //return (false,"Payment failed due to insufficient funds."); // Simulate a failure case
        }
    }
}
