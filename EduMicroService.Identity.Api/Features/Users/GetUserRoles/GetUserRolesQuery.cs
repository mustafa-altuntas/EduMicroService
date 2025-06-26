using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.GetUserRoles;
public sealed record GetUserRolesQuery(Guid UserId) : IRequestByServiceResult<List<string>>;
