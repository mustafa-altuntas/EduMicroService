using EduMicroService.Shared;

namespace EduMicroService.Identity.Api.Features.Users.RegisterUser
{
    public record RegisterUserCommand(string Email, string Password):IRequestByServiceResult<Guid>;
}
