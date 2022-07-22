namespace Domain
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Olive;

    /// <summary>
    /// Provides the business logic for Extensions class.
    /// </summary>
    public static class Extensions
    {
        public static async Task ToggleArchive(this IArchivable archivableObject)
        {
            await Context.Current.Database().Update(archivableObject, x =>
            {
                x.IsDeactivated = !archivableObject.IsDeactivated;
            });
        }
    }
}