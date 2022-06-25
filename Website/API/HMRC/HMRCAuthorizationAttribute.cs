using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Olive;
using System;

namespace Website.API
{
    internal class HMRCAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                context.Result = new ForbidResult();
                return;
            }

            var apiTokenHeaderValue = context.HttpContext.Request.Headers["Authorization"].ToString();

            var apiTokenSettingsValue = Config.Get<string>("Integration:HMRC:APIToken");

            if (apiTokenHeaderValue.IsEmpty() || !apiTokenSettingsValue.Equals(apiTokenHeaderValue, StringComparison.Ordinal))
                context.Result = new ForbidResult();
        }
    }
    internal class HMRCAuthorizationAttribute : TypeFilterAttribute
    {
        public HMRCAuthorizationAttribute() : base(typeof(HMRCAuthorizationFilter))
        {

        }
    }
}