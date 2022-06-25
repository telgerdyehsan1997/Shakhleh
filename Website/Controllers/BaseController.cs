using Domain;
using Olive.Mvc;
using Olive.Security;
using Olive.Web;
using Olive;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Http;

namespace Controllers
{
    // TODO: Uncomment this if you want to support JWT authentication, for example for WebAPI clients.
    //[JwtAuthenticate]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseController : Olive.Mvc.Controller
    {
        // TODO: Uncomment this if you want to use ApiClient
        // ApiClient.FallBack.Handle(arg => Notify(arg.FriendlyMessage, false));

        /// <summary>
        /// Gets a Domain User object extracted from the current user principal.
        /// </summary>
        [NonAction]
        public User GetUser() => User.Extract<User>();

        public CompanyUser CompanyUser => GetUser() as CompanyUser;
        public ChannelPortsUser ChannelPortsUser => GetUser() as ChannelPortsUser;


        [NonAction]
        internal string SaveInSession(string item, string fieldname)
        {
            var key = fieldname + "_contactId_" + User.GetId();
            HttpContext.Session.SetString(key, item);

            return key;
        }

        [NonAction]
        internal string GetFromSession(string key) => HttpContext.Session.GetString(key);

        [NonAction]
        internal void ClearSession(string key) => HttpContext.Session.Remove(key);

    }
}

namespace ViewComponents
{
    public abstract class ViewComponent : Olive.Mvc.ViewComponent
    {

        /// <summary>
        /// Gets a Domain User object extracted from the current user principal.
        /// </summary>
        public User GetUser() => Context.Current.Http().User.Extract<User>();

        public CompanyUser CompanyUser => GetUser() as CompanyUser;
        public ChannelPortsUser ChannelPortsUser => GetUser() as ChannelPortsUser;
    }
}