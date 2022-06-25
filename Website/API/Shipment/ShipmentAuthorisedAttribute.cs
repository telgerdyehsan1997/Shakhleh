using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Olive;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Website.API
{
    internal class ShipmentAuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Result = new ForbidResult();
                    return;
                }

                var jwt = context.HttpContext.Request.Headers["Authorization"].ToString();
                var jwtProvider = Context.Current.GetService<IJWTProvider>().Of(JWTProviderType.Shipment);

                var (isValid, username) = jwtProvider.ValidateToken(jwt.Remove("Bearer ")).GetAwaiter().GetResult();

                if (!isValid)
                    context.Result = new ForbidResult();
                else
                    Context.Current.Http().Items.Add("api-user", username);
            }
            catch (Exception)
            {
                context.Result = new ForbidResult();
                throw;
            }
        }
    }
    internal class ShipmentAuthorizeAtribute : TypeFilterAttribute
    {
        public ShipmentAuthorizeAtribute() : base(typeof(ShipmentAuthorizeFilter))
        {

        }
    }
}