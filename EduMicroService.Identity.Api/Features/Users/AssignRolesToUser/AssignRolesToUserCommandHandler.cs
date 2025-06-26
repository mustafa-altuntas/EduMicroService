using EduMicroService.Identity.Api.Features.Roles;
using EduMicroService.Identity.Api.Models;
using EduMicroService.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace EduMicroService.Identity.Api.Features.Users.AssignRolesToUser;

public class AssignRolesToUserCommandHandler(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    : IRequestHandler<AssignRolesToUserCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(AssignRolesToUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
            return ServiceResult.ErrorAsNotFound();

        var allRoles = roleManager.Roles.Select(r => r.Name).ToHashSet(StringComparer.OrdinalIgnoreCase);
        var invalidRoles = request.Roles.Where(r => !allRoles.Contains(r)).ToList();

        if (invalidRoles.Any())
        {
            return ServiceResult.Error(
                "Invalid roles",
                $"The following roles are invalid: {string.Join(", ", invalidRoles)}",
                HttpStatusCode.BadRequest
            );
        }

        var currentRoles = await userManager.GetRolesAsync(user);
        var removeResult = await userManager.RemoveFromRolesAsync(user, currentRoles);
        if (!removeResult.Succeeded)
            return ServiceResult.Error("Failed to remove existing roles", removeResult);

        var addResult = await userManager.AddToRolesAsync(user, request.Roles);
        if (!addResult.Succeeded)
            return ServiceResult.Error("Failed to assign new roles", addResult);

        return ServiceResult.SuccessAsNoContent();
    }
}
