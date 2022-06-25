using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class LookupService : ILookupService
    {
        IDatabase Database;
        public LookupService(IDatabase database)
        {
            Database = database;
        }

        public Task<IEnumerable<Company>> GetActiveCompanyList()
        {
            return Database.Of<Company>().Where(item => !item.IsDeactivated && item.IsCreatedFromAPI == false
               && item.Type.IsAnyOf(CompanyType.Customer, CompanyType.Forwarder, CompanyType.Flex)).OrderBy(t => t.Name).GetList();
        }
    }
}
