using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.GetUsers;

public record GetUsersQuery() : IRequestByServiceResult<List<UserDto>>;

