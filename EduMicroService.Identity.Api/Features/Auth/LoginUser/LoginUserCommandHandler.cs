using EduMicroService.Identity.Api.Dto;
using EduMicroService.Identity.Api.Features.Roles;
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

namespace EduMicroService.Identity.Api.Features.Users.LoginUser
{
    public class LoginUserCommandHandler(IOptions<CustomTokenOption> options, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, AppDbContext context) : IRequestHandler<LoginUserCommand, ServiceResult<AuthResponse>>
    {
        private readonly CustomTokenOption _tokenOption = options.Value;
        public async Task<ServiceResult<AuthResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            var appUser = await userManager.FindByEmailAsync(request.Email);
            if (appUser is null)
            {
                return ServiceResult<AuthResponse>.Error("User not found", "", HttpStatusCode.BadRequest);
            }

            var signInResult = await signInManager.CheckPasswordSignInAsync(appUser, request.Password, false);
            if (!signInResult.Succeeded)
            {
                return ServiceResult<AuthResponse>.Error("Password is wrong", "", HttpStatusCode.BadRequest);
            }


            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var roles = await userManager.GetRolesAsync(appUser);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: _tokenOption.Issuer,
                    expires: accessTokenExpiration,
                    notBefore: DateTime.Now,
                    claims: GetClaims(appUser, roles, _tokenOption.Audiences),
                    signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var authResponse = new AuthResponse(token, CreateRefreshToken(), accessTokenExpiration);

            var userRefreshToken = await context.UserRefreshTokens.FirstOrDefaultAsync(x => x.UserId == appUser.Id, cancellationToken: cancellationToken);
            if (userRefreshToken is not null)
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
            var numberByte = new Byte[32];

            using var rnd = RandomNumberGenerator.Create();

            rnd.GetBytes(numberByte);

            return Convert.ToBase64String(numberByte);
        }

        private IEnumerable<Claim> GetClaims(AppUser appUser, IList<string> roles, List<String> audiences)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, appUser.Email!),
            new Claim(ClaimTypes.Name,appUser.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };

            if (roles != null)
            {
                roles.ToList().ForEach(role =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                });
            }


            claims.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));

            return claims;
        }




    }
}
