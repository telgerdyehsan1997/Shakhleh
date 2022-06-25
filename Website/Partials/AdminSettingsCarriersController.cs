using Domain;
using Olive;
using Olive.Entities;
using Olive.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Controllers
{
    partial class AdminSettingsCarriersController
    {
        Olive.DatabaseFilters<Carrier> DatabaseFilters;
        public async Task<IDatabaseQuery<Carrier>> FilterCarrier(vm.CarrierList info)
        {
            DatabaseFilters = new DatabaseFilters<Carrier>();

            if (info.Name.HasValue())
                DatabaseFilters.Add(c => c.Name == info.Name || c.Name.Contains(info.Name, false));

            if (info.IsDeactivated.HasValue)
                DatabaseFilters.Add(x => x.IsDeactivated == info.IsDeactivated);

            var query = Database.Of<Carrier>().Where(DatabaseFilters).OrderBy(item => item.Name);

            info.Paging.TotalItemsCount = await query.Count();
            return query.Sort(info.Sort).Page(info.Paging);
        }
    }
}
