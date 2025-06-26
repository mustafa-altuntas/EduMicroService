using EduMicroService.Identity.Api.Dto;
using EduMicroService.Identity.Api.Options;
using EduMicroService.Shared;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace EduMicroService.Identity.Api.Features.Users.LoginClient
{
    public class LoginClientCommandHandler(IOptions<List<ClientTokenOption>> clientOptions, IOptions<CustomTokenOption> accessTokenOptions) : IRequestHandler<LoginClientCommand, ServiceResult<AuthResponse>>
    {
        private readonly List<ClientTokenOption> _clientTokenOption = clientOptions.Value;
        private readonly CustomTokenOption _tokenOption = accessTokenOptions.Value;

        public async Task<ServiceResult<AuthResponse>> Handle(LoginClientCommand request, CancellationToken cancellationToken)
        {

            var client = _clientTokenOption.FirstOrDefault(x => x.ClientId == request.ClientId && x.ClientSecret == request.ClientSecret);
            if (client is null)
            {
                return ServiceResult<AuthResponse>.Error("Client not found", "", HttpStatusCode.BadRequest);
            }

            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var authResponse = new AuthResponse(token, Guid.Empty.ToString(), accessTokenExpiration);



            return ServiceResult<AuthResponse>.SuccessAsOk(authResponse);

        }

        private IEnumerable<Claim> GetClaimsByClient(ClientTokenOption client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(c => new Claim(JwtRegisteredClaimNames.Aud, c)));

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //claims.Add(new Claim(JwtRegisteredClaimNames.Jti, client.ClientId));
            claims.Add(new Claim(ClaimTypes.Role, "client"));
            claims.Add(new Claim(ClaimTypes.Role, client.ClientId));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, client.ClientId));

            return claims;
        }





    }
}
