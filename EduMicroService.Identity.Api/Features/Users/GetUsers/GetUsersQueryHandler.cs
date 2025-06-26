using EduMicroService.Identity.Api.Models;
using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using EduMicroService.Identity.Api.Dto;
using Microsoft.EntityFrameworkCore;

namespace EduMicroService.Identity.Api.Features.Users.GetUsers;

public class GetUsersQueryHandler(UserManager<AppUser> userManager, IMapper mapper) : IRequestHandler<GetUsersQuery, ServiceResult<List<UserDto>>>
{
    public async Task<ServiceResult<List<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userManager.Users.ToListAsync(cancellationToken);
        if (users == null || users.Count == 0)
        {
            users = new();
        }
        return ServiceResult<List<UserDto>>.SuccessAsOk(mapper.Map<List<UserDto>>(users));
    }
}

