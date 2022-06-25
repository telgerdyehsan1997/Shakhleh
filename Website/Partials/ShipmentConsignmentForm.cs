using Domain;
using Microsoft.AspNetCore.Mvc;
using Olive;
using Olive.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ViewModel;
using vm = ViewModel;

namespace Controllers
{
    partial class ConsignmentsConsignmentEnterController
    {
        public async Task<Company> GetGuarantor(vm.ConsignmentForm info)
        {
            var shipmentCompany = info.SetShipment.Company;

            if (shipmentCompany.GuarantorTypeId == GuarantorType.Own)
                return shipmentCompany;

            if (shipmentCompany.GuarantorTypeId == GuarantorType.DifferentCompanyGuarantee)
                return shipmentCompany.GuarantorName;

            var ukTradder = info.UKTrader;

            if (ukTradder != null)
            {
                if (ukTradder.GuarantorTypeId == GuarantorType.Own)
                    return ukTradder;

                if (ukTradder.GuarantorTypeId == GuarantorType.DifferentCompanyGuarantee)
                    return ukTradder.GuarantorName;
            }
            return await Company.ChannelPort;
        }


        private string GetReturnURL(vm.ConsignmentForm info)
        {
            var currentURL = Url.Current();
            var nameValues = HttpUtility.ParseQueryString(Request.QueryString.ToString());

            if (nameValues["item"] != null)
                return currentURL;

            var absolutePath = Url.CurrentUri().AbsolutePath;

            return $"{absolutePath}/{info.Item.ID}?{nameValues}";
        }

        private Task<IEnumerable<TermOfSale>> GetTermOfSales()
        {
            var query = Database.Of<TermOfSale>().Where(x => !x.IsDeactivated);
            // Commented based on : 135272
            //if (info.Shipment.IsOutUK)
            //    query = query.Where(x => !x.IsDDP);
            return query.GetList();
        }

        private Task<IEnumerable<CPC>> GetSpecialCPCs(vm.ConsignmentForm info)
        {
            return Helper.GetSpecialCPCs(info.Shipment.Company, info.UKTrader)
                .Where(x => x.TypeId == info.Shipment.TypeId);
        }

        private async Task ChangeUKTrader(ConsignmentForm info)
        {
            if (info.SetShipment.Company.Type == CompanyType.Forwarder)
            {
                if (info.UKTrader?.DefaultDeclarant?.EORINumber?.HasValue() == true)
                {
                    info.Declarant = info.UKTrader?.DefaultDeclarant;
                    info.Declarant_Text = info.UKTrader?.DefaultDeclarant.ToStringOrEmpty();
                }
            }
            else
            {
                var declarant = info.UKTrader == info.SetShipment.Company ? info.UKTrader?.DefaultDeclarant : info.SetShipment.Company;
                if (declarant != null && declarant.EORINumber.HasValue())
                {
                    info.Declarant = declarant;
                    info.Declarant_Text = declarant.ToStringOrEmpty();
                }
            }
            if (info.Declarant == null)
            {
                info.Declarant = await Company.ChannelPort;
                info.Declarant_Text = info.Declarant.ToStringOrEmpty();
            }

            if (!info.HasLoaded)
            {
                info.HasLoaded = true;
                info.UseEIDR = true;
            }
            if (info.SequenceNumber.HasValue() && info.UseEIDR)
                info.Item.UCR = info.Item.CreateDUCR(info.SetShipment.Company.AuthorisationNumber, info.SequenceNumber);

            else if (info.UKTrader?.CFSPTypeId.IsAnyOf(CFSPType.Channelports) == true && info.UKTrader?.UsingEIDR == true)
            {
                info.Item.UCR = info.Item.UCR.Or(info.Item.CreateUCR((await Company.ChannelPort).EORINumber, Settings.Current.ChannelportsCFSPShipmentNumber));
                info.Item.CFSPShipmentNumber = info.Item.CFSPShipmentNumber.Or(info.Item.UCR.Split("-")[1]);

            }

            else
                info.Item.UCR = info.Item.CreateUCR(info.Declarant?.EORINumber, info.ConsignmentNumber);
        }

        private async Task ChangeDeclarant(ConsignmentForm info)
        {
            if (info.SequenceNumber.HasValue() && info.UseEIDR)
                info.Item.UCR = info.Item.CreateDUCR(info.SetShipment.Company.AuthorisationNumber, info.SequenceNumber);

            else if (info.UKTrader.CFSPTypeId.IsAnyOf(CFSPType.Channelports) && info.UKTrader.UsingEIDR == true)
            {
                info.Item.UCR = info.Item.UCR.Or(info.Item.CreateUCR((await Company.ChannelPort).EORINumber, Settings.Current.ChannelportsCFSPShipmentNumber));
                info.Item.CFSPShipmentNumber = info.Item.CFSPShipmentNumber.Or(info.Item.UCR.Split("-")[1]);

            }
            else
                info.Item.UCR = info.Item.CreateUCR(info.Declarant?.EORINumber, info.ConsignmentNumber);
        }


        //private async Task OnBoundNew(ConsignmentForm info)
        //{
        //    if (!info.HasCFSP && info.Item.IsNew && info.IsCFSPShipmentNumberVisible && info.CFSPShipmentNumber.IsEmpty())
        //    {
        //        info.HasCFSP = true;
        //        info.CFSPShipmentNumber = await info.Item.SetCFSPShipmentNumber();
        //    }
        //    if (info.IsCFSPShipmentNumberVisible && info.CFSPShipmentNumber.IsEmpty() && !info.Item.IsNew)
        //        info.CFSPShipmentNumber = await info.Item.SetCFSPShipmentNumber();
        //}


        private async Task OnBoundGet(ConsignmentForm info)
        {
            if (info.Item.IsNew)
            {
                if (info.SetShipment.Company.Type != CompanyType.Forwarder)
                {
                    var trader = info.SetShipment.Company;
                    if (trader.IsCreatedFromAPI == false && trader.EORINumber.HasValue() && trader.EORINumber != Constants.ChannelPortEORI && trader.EORINumber.StartsWith("GB"))
                        info.UKTrader = info.SetShipment.Company;


                    var declarent = info.UKTrader == info.SetShipment.Company ? info.UKTrader?.DefaultDeclarant : info.SetShipment.Company;

                    if (declarent != null && declarent.IsCreatedFromAPI == false && declarent.EORINumber.HasValue() && declarent.EORINumber.StartsWith("GB"))
                    {
                        info.Declarant = declarent;
                        info.Declarant_Text = declarent.ToStringOrEmpty();
                    }
                    if (info.Declarant == null)
                    {
                        info.Declarant = await Company.ChannelPort;
                        info.Declarant_Text = info.Declarant.ToStringOrEmpty();
                    }
                }
                else
                {
                    if (info.SetShipment.Company.EORINumber.HasValue())
                    {
                        info.Declarant = info.SetShipment.Company.DefaultDeclarant;
                        info.Declarant_Text = info.SetShipment.Company.DefaultDeclarant.ToStringOrEmpty();
                    }
                }

                var cfspNumber = "";
                if (Settings.Current.ChannelportsCFSPShipmentNumber.IsEmpty())
                    cfspNumber = await Settings.SetCFSPShipmentNumber();

                if (info.SequenceNumber.HasValue() && info.UseEIDR)
                    info.Item.UCR = info.Item.CreateDUCR(info.SetShipment.Company.AuthorisationNumber, info.SequenceNumber);

                else if (info.UKTrader != null && info.UKTrader?.CFSPTypeId.IsAnyOf(CFSPType.Channelports) == true && info.UKTrader?.UsingEIDR == true)
                {
                    info.Item.UCR = info.Item.CreateUCR((await Company.ChannelPort).EORINumber, Settings.Current.ChannelportsCFSPShipmentNumber.Or(cfspNumber));
                    info.Item.CFSPShipmentNumber = info.Item.UCR.Split("-")[1];

                    if (await Database.Of<Consignment>().Any(x => x.UCR == info.Item.UCR))
                    {
                        cfspNumber = await Settings.SetCFSPShipmentNumber();
                        info.Item.UCR = info.Item.CreateUCR((await Company.ChannelPort).EORINumber, cfspNumber);
                        info.Item.CFSPShipmentNumber = info.Item.UCR.Split("-")[1];
                    }
                }
                else
                    info.Item.UCR = info.Item.CreateUCR(info.Declarant?.EORINumber, info.ConsignmentNumber);
            }
        }

        private async Task OnPostBound(ConsignmentForm info)
        {
            var newSelectedUKTrader = await (GetFromSession("uktrader_contactId_" + User.GetId())
                .To<Guid>()).To<Company>();
            if (newSelectedUKTrader != null)
            {
                info.UKTrader = newSelectedUKTrader;
                info.UKTrader_Text = newSelectedUKTrader.GetUkDisplayText();
            }

            var newSelectedPartner = await (GetFromSession("partner_contactId_" + User.GetId())
                .To<Guid>()).To<Company>();
            if (newSelectedPartner != null)
            {
                info.Partner = newSelectedPartner;
                info.Partner_Text = newSelectedPartner.GetPatnerText();
            }

            var newSelectedDeclarant = await (GetFromSession("declarant_contactId_" + User.GetId())
                .To<Guid>()).To<Company>();
            if (newSelectedDeclarant != null)
            {
                info.Declarant = newSelectedDeclarant;
                info.Declarant_Text = newSelectedDeclarant.ToStringOrEmpty();
            }
            if (info.SequenceNumber.HasValue() && info.UseEIDR)
                info.Item.UCR = info.Item.IsNew ? info.Item.CreateDUCR(info.SetShipment.Company.AuthorisationNumber, info.SequenceNumber) : info.Item.UCR;

            else if (info.UKTrader != null && info.UKTrader.CFSPTypeId.IsAnyOf(CFSPType.Channelports) && info.UKTrader.UsingEIDR == true)
            {
                info.Item.UCR = info.Item.UCR.Or(info.Item.CreateUCR((await Company.ChannelPort).EORINumber, Settings.Current.ChannelportsCFSPShipmentNumber));
                info.Item.CFSPShipmentNumber = info.Item.CFSPShipmentNumber.Or(info.Item.UCR.Split("-")[1]);
            }
            else
                info.Item.UCR = info.Item.IsNew ? info.Item.CreateUCR(info.Declarant?.EORINumber, info.ConsignmentNumber) : info.Item.UCR;

            info.ImporterOrExporter = info.SetShipment.IsInUK ? "importer" : "exporter";



            ClearSession();
        }

        private void ClearSession()
        {
            ClearSession("uktrader_contactId_" + User.GetId());
            ClearSession("partner_contactId_" + User.GetId());
            ClearSession("declarant_contactId_" + User.GetId());
        }

        public async Task UpdateUkTraderAndPartnersToCompany(ConsignmentForm info, Consignment oldInfo)
        {

            if (oldInfo.UKTraderId.HasValue && oldInfo.UKTraderId != info.UKTrader)
            {
                var ukTrader = await info.Shipment.Company.UKTradersAndPartners
                    .GetList().FirstOrDefault(x => x.UKTraderPartnerId == oldInfo.UKTrader);
                if (ukTrader != null)
                {
                    var currentCompanyUkTraders = await oldInfo.Shipment.Company.UKTradersAndPartners
                    .Where(x => x.TypeId == CompanyUKTraderPartnerLinkType.UKTrader && x.ConsignmentId != oldInfo)
                    .GetList();

                    if (ukTrader.IsNoneOf(currentCompanyUkTraders))
                        await Database.Delete(ukTrader);
                }
            }
            if (info.UKTrader != info.Shipment.Company)
            {
                var ukTrader = new CompanyUKTraderPartnerLink
                {
                    UKTraderPartner = info.UKTrader,
                    Company = info.Shipment.Company,
                    Type = CompanyUKTraderPartnerLinkType.UKTrader,
                    Consignment = info.Item
                };

                var existingUkTraders = info.Shipment.Company.UKTradersAndPartners.Where(x => x.TypeId == CompanyUKTraderPartnerLinkType.UKTrader).GetList();
                if (ukTrader.IsNoneOf(await existingUkTraders))
                    await Database.Save(ukTrader);
            }
            if (oldInfo.PartnerId.HasValue && oldInfo.PartnerId != info.Partner)
            {
                var partner = await info.Shipment.Company.UKTradersAndPartners
                  .GetList().FirstOrDefault(x => x.UKTraderPartnerId == oldInfo.Partner);

                if (partner != null)
                {
                    var currentCompanyPartners = await oldInfo.Shipment.Company.UKTradersAndPartners
                    .Where(x => x.TypeId == CompanyUKTraderPartnerLinkType.Partner && x.ConsignmentId != oldInfo)
                    .GetList();

                    if (partner.IsNoneOf(currentCompanyPartners))
                        await Database.Delete(partner);
                }
            }
            if (info.Partner != info.Shipment.Company)
            {
                var partner = new CompanyUKTraderPartnerLink
                {
                    UKTraderPartner = info.Partner,
                    Company = info.Shipment.Company,
                    Type = CompanyUKTraderPartnerLinkType.Partner,
                    Consignment = info.Item
                };

                var existingPartners = info.Shipment.Company.UKTradersAndPartners.Where(x => x.TypeId == CompanyUKTraderPartnerLinkType.Partner).GetList();
                if (partner.IsNoneOf(await existingPartners))
                    await Database.Save(partner);
            }
        }

    }


}