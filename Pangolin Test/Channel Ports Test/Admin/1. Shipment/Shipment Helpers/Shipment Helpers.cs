using Pangolin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    public static class ShipmentHelpers
    {
        public static void NavigateToShipments(this UITest @this)
        {
            @this.ClickLink("Shipments");
            @this.ExpectHeader("Shipments");
        }

        public static void FindShipment(this UITest @this, string trackingNumber)
        {
            @this.Set("Date Created").To("01/01/1999");
            @this.Set(The.Top, "to").To("25/12/2025");
            @this.ClickButton("Search");
            @this.Expect(trackingNumber);
        }

        public static void CreateNewShipment(this UITest @this, string companyName, string shipmentType, string shipmentRoute, string primaryContact,
            string customerReference, string vehicleNumber, string expectedDate, string isNcts = null, string notifyParties = null,
            string groupType = null, string contactName = null, string safetyAndSecurity = null, string containerNumber = null)
        {
            @this.ClickLink("New Shipment");
            @this.ExpectHeader("Shipment Details");
            @this.ClickHeader("Shipment Details");

            @this.ClickField("Company Name");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(companyName);
            System.Threading.Thread.Sleep(1000);
            @this.Click(companyName);
            @this.ClickHeader("Shipment Details");
            System.Threading.Thread.Sleep(1000);

            @this.AtLabel(The.Top, "Type").ClickLabel(shipmentType);
            if (shipmentType == ShipmentTypeConstants.OutUK)
            {
                @this.ExpectLabel("NCTS");
                @this.ExpectLabel("Expected date of departure");
                if (isNcts == "Yes")
                {
                    @this.AtLabel("NCTS").ClickLabel("Yes");
                }
                else
                {
                    @this.AtLabel("NCTS").ClickLabel("No");
                }
            }
            else
            {
                @this.ExpectNoLabel("NCTS");
            }

            if (safetyAndSecurity.HasAny())
            {
                @this.ExpectLabel("Safety and security");
                @this.AtLabel("Safety and security").ClickLabel(safetyAndSecurity);
                if (safetyAndSecurity == "Yes")
                {
                    @this.ExpectField("Container Number");
                    @this.Set("Container Number").To(containerNumber);
                }
            }

            @this.AtLabel("Notify additional parties").ClickLabel(notifyParties);
            if (notifyParties == AdditionalPartyConstants.Group)
            {
                @this.Set(The.Bottom, "Group").To(groupType);
            }
            else if (notifyParties == AdditionalPartyConstants.SpecificContacts)
            {
                @this.Set("Contact name").To(contactName);
            }
            else
                @this.AtLabel("Notify additional parties").ClickLabel(AdditionalPartyConstants.NotRequired);

            @this.ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(shipmentRoute);
            System.Threading.Thread.Sleep(1000);
            @this.Click(shipmentRoute);

            if (safetyAndSecurity.HasAny())
            {
                @this.ExpectLabel("Safety and security");
                @this.AtLabel("Safety and security").ClickLabel(safetyAndSecurity);
            }

            @this.ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(primaryContact);
            System.Threading.Thread.Sleep(1000);
            @this.Click(primaryContact);

            @this.Set("Customer Reference").To(customerReference);
            @this.Set("Vehicle number").To(vehicleNumber);

            @this.Click("Save and Add/Amend Consignments");
            System.Threading.Thread.Sleep(1000);
            @this.Type(expectedDate);
            System.Threading.Thread.Sleep(1000);
            @this.Click("Save and Add/Amend Consignments");
            @this.ExpectHeader("Consignment Details");

        }

        public static void AddConsignmentToShipment(this UITest @this, string consignmentNumber, string ukTrader, string partnerName, string declarantName, string totalPackages, string totalGrossWeight, string totalNetWeight, string invoiceNumber, string invoiceCurrency, string totalValue, string termsOfSale)
        {
            @this.ExpectHeader("Consignment Details");
            @this.ClickHeader("Consignment Details");

            @this.Set("Uk trader").To("");
            @this.ClickHeader("Consignment Details");
            @this.ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(ukTrader);
            System.Threading.Thread.Sleep(1000);
            @this.Click(ukTrader);

            @this.ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(partnerName);
            System.Threading.Thread.Sleep(1000);
            @this.Click(partnerName);

            @this.Set("Declarant").To("");
            @this.ClickHeader("Consignment Details");
            @this.ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(declarantName);
            System.Threading.Thread.Sleep(1000);
            @this.Click(declarantName);

            @this.Set("Total packages").To(totalPackages);
            @this.Set("Total gross weight").To(totalGrossWeight);
            @this.Set("Total net weight").To(totalNetWeight);
            @this.Set("Invoice number").To(invoiceNumber);

            @this.ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(invoiceCurrency);
            System.Threading.Thread.Sleep(1000);
            @this.Click(invoiceCurrency);

            @this.Set("Total value").To(totalValue);

            @this.Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(What.Contains, termsOfSale);
            System.Threading.Thread.Sleep(1000);
            @this.Click(What.Contains, termsOfSale);

            @this.Click("Save and Add Commodities");
            @this.ExpectHeader(That.Contains, consignmentNumber);
        }

        public static void AddSFDConsignment(this UITest @this, string ukTrader, string partnerName, string declarantName, string totalPackages, string invoiceNumber,
            string cfspShipmentNumber = null)
        {
            @this.ExpectHeader("Consignment Details");
            @this.Set("UK trader").To("");
            @this.ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);

            @this.ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(ukTrader);
            System.Threading.Thread.Sleep(1000);
            @this.Click(ukTrader);

            @this.ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(partnerName);
            System.Threading.Thread.Sleep(1000);
            @this.Click(partnerName);

            @this.Set("Declarant").To("");
            @this.ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            @this.ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(declarantName);
            System.Threading.Thread.Sleep(1000);
            @this.Click(declarantName);
            @this.ExpectField("CFSP Shipment Number");
            @this.Set("CFSP Shipment Number").To(cfspShipmentNumber);

            @this.Set("Total packages").To(totalPackages);
            @this.Set("Invoice number").To(invoiceNumber);
            @this.Click("Save and Add Commodities");

            @this.ExpectLink("New Commodity");
        }
    }
}