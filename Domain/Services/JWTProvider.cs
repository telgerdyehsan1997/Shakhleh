using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using Olive;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum JWTProviderType
    {
        ChannelPort = 1,
        Shipment = 2
    }

    public interface IJWTProvider
    {
        IJWTProviderOf Of(JWTProviderType type);
    }

    public interface IJWTProviderOf
    {
        string GenerateToken(string userName = "");

        Task<(bool IsValid, string Username)> ValidateToken(string token);
    }

    public class JWTProvider : IJWTProvider, IJWTProviderOf
    {
        string Secret;
        int Expiry;
        JWTProviderType Type;
       
        public IJWTProviderOf Of(JWTProviderType type)
        {
            Secret = Config.Get<string>($"Integration:{type}:Secret");
            Expiry = Config.Get<int>($"Integration:{type}:Expiry");
            Type = type;
            return this;
        }

        public string GenerateToken(string username = "")
        {
            var userName = GetUserName(username);

            var symmetricKey = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(symmetricKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            if (Expiry > 0)
                tokenDescriptor.Expires = now.AddMinutes(Expiry);

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(stoken);
        }

        private string GetUserName(string username)
        {
            return Type == JWTProviderType.ChannelPort ? Config.Get<string>($"Integration:{Type}:User") : username;
        }

        public async Task<(bool IsValid, string Username)> ValidateToken(string token)
        {
            var simplePrinciple = GetPrincipal(token);
            if (simplePrinciple == null)
            {
                return (false, null);
            }
            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return (false, null);

            if (!identity.IsAuthenticated)
                return (false, null);

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            var username = usernameClaim?.Value;
            return (true, username);
        }

        ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(Secret);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }
            catch (Exception ex)
            {
                Log.For<SecurityToken>().Error(ex);
                return null;
            }
        }
    }
}
