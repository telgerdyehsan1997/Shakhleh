namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class SupportTicket
    {
        protected override async Task OnValidating(EventArgs e)
        {
            await base.OnValidating(e);

            //var emailcc = await this.EmailCC.GetList().Select(x => x.EmailCc).ToList();
            //foreach (var email in emailcc)
            //{
            //    if (!Helper.EmailIsValid(email))
            //        throw new ValidationException(email + " is not a valid Email address");
            //}
        }
    }
}