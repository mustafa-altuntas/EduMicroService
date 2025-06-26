using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Roles.GetUsersInRole;
public record GetUsersInRoleQuery(string Name) : IRequestByServiceResult<List<UserDto>>;


