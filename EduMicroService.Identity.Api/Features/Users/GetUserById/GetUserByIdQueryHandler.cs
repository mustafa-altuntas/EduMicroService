using EduMicroService.Identity.Api.Models;
using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using EduMicroService.Identity.Api.Dto;
using AutoMapper;

namespace EduMicroService.Identity.Api.Features.Users.GetUserById;

public class GetUserByIdQueryHandler(UserManager<AppUser> userManager, IMapper mapper) : IRequestHandler<GetUserByIdQuery, ServiceResult<UserDto>>
{
    public async Task<ServiceResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
        {
            return ServiceResult<UserDto>.ErrorAsNotFound();
        }
        return ServiceResult<UserDto>.SuccessAsOk(mapper.Map<UserDto>(user));
    }
 
}

