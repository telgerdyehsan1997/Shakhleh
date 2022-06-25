using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Text.RegularExpressions;

namespace Website.API
{
    public class LRNPatternRouteCheker : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out object value) && value != null)
            {
                return Regex.IsMatch(value.ToString(), "^[a-zA-Z0-9]{1,22}$", RegexOptions.Singleline);
            }
            return false;
        }
    }
}
