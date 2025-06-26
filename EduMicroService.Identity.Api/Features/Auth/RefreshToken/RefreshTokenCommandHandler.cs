using EduMicroService.Identity.Api.Dto;
using EduMicroService.Identity.Api.Models;
using EduMicroService.Identity.Api.Options;
using EduMicroService.Identity.Api.Repositories;
using EduMicroService.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EduMicroService.Identity.Api.Features.Auth.RefreshToken
{
    public class RefreshTokenCommandHandler(IOptions<CustomTokenOption> options, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, AppDbContext context) : IRequestHandler<RefreshTokenCommand, ServiceResult<AuthResponse>>
    {
        private readonly CustomTokenOption _tokenOption = options.Value;
        public async Task<ServiceResult<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {



            var refreshToken = await context.UserRefreshTokens.FirstOrDefaultAsync(rt=>rt.Code == request.RefreshToken);
            if (refreshToken == null)
            {
                return ServiceResult<AuthResponse>.Error("Refresh token is not valid", "", HttpStatusCode.BadRequest);
            }
            if (refreshToken.Expiration < DateTime.Now)
            {
                return ServiceResult<AuthResponse>.Error("Refresh token is expired", "", HttpStatusCode.BadRequest);
            }

            var appUser = await userManager.FindByIdAsync(refreshToken.UserId.ToString());

            if (appUser is null)
            {
                return ServiceResult<AuthResponse>.Error("User not found", "", HttpStatusCode.BadRequest);
            }



            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: _tokenOption.Issuer,
                    expires: accessTokenExpiration,
                    notBefore: DateTime.Now,
                    claims: GetClaims(appUser, _tokenOption.Audiences),
                    signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var authResponse = new AuthResponse(token, CreateRefreshToken(), accessTokenExpiration);

            var userRefreshToken = await context.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == appUser.Id, cancellationToken: cancellationToken);
            if(userRefreshToken is not null)
            {
                userRefreshToken.Expiration = refreshTokenExpiration;
                userRefreshToken.Code = authResponse.RefreshToken;
            }
            else
            {
                context.UserRefreshTokens.Add(new UserRefreshToken
                {
                    UserId = appUser.Id,
                    Code = authResponse.RefreshToken,
                    Expiration = refreshTokenExpiration
                });
            }
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<AuthResponse>.SuccessAsOk(authResponse);

        }



        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(AppUser appUser, List<string> audiences)
        {
            var userList = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, appUser.Email!),
            new Claim(ClaimTypes.Name,appUser.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return userList;
        }




    }
}
