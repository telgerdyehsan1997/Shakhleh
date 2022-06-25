using Domain;
using Microsoft.OpenApi.Writers;
using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Controllers
{
    public partial class ShipmentEnterController
    {

        public async Task ReloadCompany(vm.ShipmentForm info)
        {
            info.Item.Company = info.Company; info.Item.PrimaryContact = null; info.PrimaryContact = null; info.PrimaryContact_Text = null;
            if (info.Company != null)
            {

                if (await info.Company.TransactionTypes.Count() == 1)
                {
                    if (await info.Company.TransactionTypes.Any(x => x.ShipmentTypeId == ShipmentType.IntoUk))
                        info.Type = ShipmentType.IntoUk;
                    else if (await info.Company.TransactionTypes.Any(x => x.ShipmentTypeId == ShipmentType.OutOfUk))
                    {
                        info.Type = ShipmentType.OutOfUk;
                        info.IsNCTSShipmentOutConvertible_Visible = true;
                    }
                }
                else
                {
                    info.Type = null;
                }

                if (await info.Company.Contacts.Count() == 1)
                {
                    info.Item.PrimaryContact = await info.Company.Contacts.FirstOrDefault();
                    info.PrimaryContact = await info.Company.Contacts.FirstOrDefault();
                    info.PrimaryContact_Text = info.PrimaryContact.Name;
                }
                else if (await info.Company.Contacts.Count() == 0 && await info.Company.CompanyUsers.Where(x => x.AccountsDepartment == false).Count() == 1)
                {
                    info.Item.PrimaryContact = await info.Company.CompanyUsers.FirstOrDefault();
                    info.PrimaryContact = await info.Company.CompanyUsers.FirstOrDefault();
                    info.PrimaryContact_Text = info.PrimaryContact.Name;
                }
            }

        }
    }
}


namespace ViewModel
{
    public partial class ShipmentForm
    {
        public bool ShowRoute => Company != null;

        public bool ShowSafetyAndSecurity()
        {
            if (Company != null)
            {
                if (Type == ShipmentType.OutOfUk && Company.SafetyAndSecurityOutboundId.IsAnyOf(Domain.SafetyAndSecurity.Sometimes))
                    return true;
                if (Type == ShipmentType.IntoUk && Company.SafetyAndSecurityInboundId.IsAnyOf(Domain.SafetyAndSecurity.Sometimes))
                    return true;
            }
            return false;
        }
        public bool AlwaysShowSafetyAndSecurity(ShipmentType shipmentType = null)
        {
            if (Company != null)
            {
                if (shipmentType != null && Type == null)
                    Type = shipmentType;

                if ((Type == ShipmentType.OutOfUk && Company.SafetyAndSecurityOutboundId.IsAnyOf(Domain.SafetyAndSecurity.Always)) || (Type == ShipmentType.IntoUk && Company.SafetyAndSecurityInboundId.IsAnyOf(Domain.SafetyAndSecurity.Always)))
                    return true;
            }
            return false;
        }
        public async Task<bool> ShowGVMS()
        {
            if (Company != null)
            {
                if (Company.GVMSId.IsAnyOf(GVMSType.Sometimes, GVMSType.Always) &&
                    ((Type == ShipmentType.IntoUk && await Route?.UKPort?.PortsIntoUk.Any(x => x.IntoUKTypeId == PortType.GVMS)) ||
                    (Type == ShipmentType.OutOfUk && Route?.UKPort.OutOfUKType == PortType.GVMS)))
                {
                    return true;
                }
            }
            return false;
        }

    }
}