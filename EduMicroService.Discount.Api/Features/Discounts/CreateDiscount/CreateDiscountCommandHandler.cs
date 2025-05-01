using EduMicroService.Discount.Api.Repositories;
using EduMicroService.Shared;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EduMicroService.Discount.Api.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHandler(AppDbContext context) : IRequestHandler<CreateDiscountCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var hasCodeForUser = await context.Discounts.AnyAsync(x => x.UserId.ToString() == request.UserId.ToString() && x.Code == request.Code, cancellationToken: cancellationToken);

            if (hasCodeForUser)
            {
                return ServiceResult<Guid>.Error("Discount code already exists for this user", HttpStatusCode.BadRequest);
            }

            var discount = new Discount()
            {
                Id = NewId.NextSequentialGuid(),
                Code = request.Code,
                Created = DateTime.Now,
                Rate = request.Rate,
                Expired = request.Expired,
                UserId = request.UserId
            };


            await context.Discounts.AddAsync(discount, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(discount.Id, "<empty>"); //todo: url 

        }
    }
}
