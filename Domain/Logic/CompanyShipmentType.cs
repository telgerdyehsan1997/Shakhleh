using Olive;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class CompanyShipmentType
    {
        public static Task<IEnumerable<Company>> NonUKCompanies()
        {
            return Database.Of<CompanyShipmentType>()
            .Where(x => x.ShipmentTypeId == ShipmentType.OutOfUk)
            .GetList()
            .Select(x => x.Company);
        }

        public static Task<IEnumerable<Company>> UKCompanies()
        {
            return Database.Of<CompanyShipmentType>()
            .Where(x => x.ShipmentTypeId == ShipmentType.IntoUk)
            .GetList()
            .Select(x => x.Company);
        }
    }
}
