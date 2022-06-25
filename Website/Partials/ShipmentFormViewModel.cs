using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModel
{
    partial class ShipmentForm
    {
        public bool IsAvalible(CompanyUser companyUser, ChannelPortsUser admin)
        {
            if (Item.IsNew || admin != null)
                return true;

            if (companyUser != null && Item.CompanyId == companyUser.Company)
                return true;

            return false;
        }
    }
}
