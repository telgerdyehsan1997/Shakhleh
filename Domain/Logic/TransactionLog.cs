namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using System.Security.Principal;

    partial class TransactionLog
    {
        public bool IsFileVisibleTo(IPrincipal user) => true;
    }

}
