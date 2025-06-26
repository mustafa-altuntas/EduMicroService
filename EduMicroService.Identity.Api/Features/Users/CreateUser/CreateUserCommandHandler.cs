using AutoMapper;
using EduMicroService.Identity.Api.Models;
using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EduMicroService.Identity.Api.Features.Users.CreateUser
{
    public class CreateUserCommandHandler(UserManager<AppUser> userManager, IMapper mapper) : IRequestHandler<CreateUserCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<AppUser>(request);
            user.UserName = user.Email!.Split('@')[0] + Guid.NewGuid().ToString().Substring(0, 4);
            var identityResult = await userManager.CreateAsync(user, request.Password);

            if (!identityResult.Succeeded)
            {
                //var user = await userManager.FindByEmailAsync(request.Email);
                //var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                //return new AuthResponse(token, token, DateTime.UtcNow.AddDays(7));


                //var errors = new Dictionary<string, object>
                //{
                //    ["errors"] = identityResult.Errors.Select(e => e.Description).ToList()
                //};


                return ServiceResult<Guid>.Error("Identity operation failed", identityResult);

            }

            // Todo : send email confirmation token and user info emal, password to user

            return ServiceResult<Guid>.SuccessAsCreated((await userManager.FindByEmailAsync(request.Email))!.Id, "");
        }
    }





}




