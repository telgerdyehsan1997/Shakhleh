namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using System.Security.Principal;

    partial class ImportQueueItem
    {
        internal async Task UpdateStatus(ImportStatus status)
        {
            var reload = await Database.Reload(this);

            await Database.Update(reload, i => i.Status = status);
        }

        public bool IsAttachmentVisibleTo(IPrincipal user) => true;

        public bool IsFileVisibleTo(IPrincipal user) => true;

    }
}