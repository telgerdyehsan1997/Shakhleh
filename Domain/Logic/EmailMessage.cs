namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class EmailMessage
    {
        public static Task PurgeEmailsOlderThan7Days()
        {
            return Database.GetAccess().ExecuteScalar("EXEC SP_PURGE_EMAILQUEUEITEMS");
        }
    }

}