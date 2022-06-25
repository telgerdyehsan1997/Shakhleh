using Controllers;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Controllers
{
    public class MFABaseController : BaseController
    {
        [NonAction]
        public async Task<bool> HasMFA(IMFAService mfaService)
        {
            var user = GetUser();
            var mfaValue = GetFromSession($"{Constants.MFAKey}_contactId_{user.GetId()}");
            var status = await mfaService.ValidateMFA(user, mfaValue);
            return status == MFAStatus.Allowed;
        }

    }
}
