using EduMicroService.Identity.Api.Dto;
using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.GetUserById;
public record GetUserByIdQuery(Guid Id) : IRequestByServiceResult<UserDto>;

