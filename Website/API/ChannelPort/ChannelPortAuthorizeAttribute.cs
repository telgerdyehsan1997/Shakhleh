using Domain;
using Microsoft.AspNetCore.Mvc;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Website.API
{
    internal class ChannelPortAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                context.Result = new ForbidResult();

            var jwt = context.HttpContext.Request.Headers["Authorization"].ToString();
            var jwtProvider = Context.Current.GetService<IJWTProvider>().Of(JWTProviderType.ChannelPort);

            var (isValid, username) = jwtProvider.ValidateToken(jwt.Remove("Bearer ")).GetAwaiter().GetResult();

            if (!isValid || username != Config.Get<string>("Integration:ChannelPort:User"))
                context.Result = new ForbidResult();
        }
    }
    internal class ChannelPortAuthorizeAttribute : TypeFilterAttribute
    {
        public ChannelPortAuthorizeAttribute() : base(typeof(ChannelPortAuthorizeFilter))
        {

        }
    }
}
