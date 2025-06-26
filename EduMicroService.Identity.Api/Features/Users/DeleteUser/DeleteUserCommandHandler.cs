using EduMicroService.Identity.Api.Models;
using EduMicroService.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduMicroService.Identity.Api.Features.Users.DeleteUser;

public class DeleteUserCommandHandler(UserManager<AppUser> userManager, IValidator<DeleteUserCommand> validator) : IRequestHandler<DeleteUserCommand, ServiceResult<Guid>>
{
    public async Task<ServiceResult<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => (object?)g.Select(e => e.ErrorMessage).ToArray()
                );

            return ServiceResult<Guid>.ErrorFromValidation400(errors);
        }


        var user = await userManager.FindByIdAsync(request.Id.ToString());

        if (user is null)
            return ServiceResult<Guid>.ErrorAsNotFound();

        var result = await userManager.DeleteAsync(user);

        if (!result.Succeeded)
            return ServiceResult<Guid>.Error("Failed to delete user", result);

        return ServiceResult<Guid>.SuccessAsOk(request.Id);
    }
}

