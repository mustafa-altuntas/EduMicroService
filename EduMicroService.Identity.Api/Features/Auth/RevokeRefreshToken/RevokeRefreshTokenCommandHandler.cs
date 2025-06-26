using EduMicroService.Identity.Api.Repositories;
using EduMicroService.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace EduMicroService.Identity.Api.Features.Auth.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommandHandler(AppDbContext context) : IRequestHandler<RevokeRefreshTokenCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(RevokeRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await context.UserRefreshTokens.FirstOrDefaultAsync(x => x.Code == request.RefreshToken);
            if (refreshToken == null)
            {
                return ServiceResult.Error("Refresh token not found.", "Refresh token not found.",HttpStatusCode.NotFound);
            }
            
            context.UserRefreshTokens.Remove(refreshToken);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
