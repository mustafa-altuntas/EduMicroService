using EduMicroService.Identity.Api.Models;
using EduMicroService.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduMicroService.Identity.Api.Features.Users.GetUserRoles;

public class GetUserRolesQueryHandler(UserManager<AppUser> userManager, IValidator<GetUserRolesQuery> validator)
    : IRequestHandler<GetUserRolesQuery, ServiceResult<List<string>>>
{
    public async Task<ServiceResult<List<string>>> Handle(GetUserRolesQuery request, CancellationToken cancellationToken)
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
            return ServiceResult<List<string>>.ErrorFromValidation400(errors);

        }


        var user = await userManager.FindByIdAsync(request.UserId.ToString());

        if (user is null)
            return ServiceResult<List<string>>.ErrorAsNotFound();

        var roles = await userManager.GetRolesAsync(user);
        if (roles is null || roles.Count == 0)
            roles = new List<string>();

        return ServiceResult<List<string>>.SuccessAsOk(roles.ToList());
    }
}
