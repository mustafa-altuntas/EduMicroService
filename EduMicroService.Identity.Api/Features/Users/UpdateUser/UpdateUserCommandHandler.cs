using EduMicroService.Identity.Api.Models;
using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduMicroService.Identity.Api.Features.Users.UpdateUser;

public class UpdateUserCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<UpdateUserCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            return ServiceResult.ErrorAsNotFound();
        }

        user.Email = request.Email;
        user.UserName = request.UserName ?? user.UserName;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            return ServiceResult.Error("Identiy operation failed",updateResult);
        }

        // Şifre güncellemesi istenmişse
        if (!string.IsNullOrWhiteSpace(request.Password))
        {
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResult = await userManager.ResetPasswordAsync(user, token, request.Password);
            if (!passwordResult.Succeeded)
            {
                return ServiceResult.Error("Identiy operation failed", passwordResult);

            }
        }

        return ServiceResult.SuccessAsNoContent();
    }
}


