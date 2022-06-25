using Pangolin;
using System;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    public static class CompaniesHelper
    {
        public static void NavigateToCompanies(this UITest @this)
        {
            @this.ClickLink("Companies");
            @this.ExpectHeader("Companies");
        }

        public static void CreateNewCompany(this UITest @this, string companyType, string companyName, string companyCountry, string postCode, string companyCity,
            string addressLine, string eoriNumber, string gvmsType, string safetyInbound, string safetyOutbound,
            string representationType, string guarantorType, string nctsType = null, string paymentType = null, string defermentNumber = null, string[] transactionTypes = null,
            string accountNumber = null, string cfspType = null, string eidrType = null, string authorisedCountry = null, string sfdOnly = null, string cfspCpcNumber = null,
            string warehouseNumber = null, string authorisationNumber = null, string vatByDan = null, string transitGuarantee = null, string guaranteeType = null,
            string guarantorName = null, string[] authorisedLocations = null)
        {
            @this.AtLabel("Type").ClickLabel(companyType);

            //Sets the Transaction types
            foreach (var transactionType in transactionTypes)
                @this.AtLabel("Transaction type(s)").ClickLabel(transactionType);

            if (transactionTypes.ContainsAny("Out of uk"))
            {
                @this.ExpectLabel("NCTS");
                @this.AtLabel("NCTS").ClickLabel(nctsType);

            }
            else
            {
                @this.ExpectNoLabel("NCTS");
            }

            @this.AtLabel("GVMS").ClickLabel(gvmsType);
            @this.AtLabel("Safety and security inbound").ClickLabel(safetyInbound);
            @this.AtLabel("Safety and security inbound").ClickLabel(safetyOutbound);

            //Adds a customer account number if Company Type is Customer, Flex or Fowarder
            if (companyType.Equals(CompanyTypeConstants.Customer))
            {
                @this.AtLabel("Type").ClickLabel("Customer");
                @this.ExpectLabel("Customer account number");
                @this.Set("Customer account number").To(accountNumber);
            }
            else
            {
                @this.ExpectNo("Customer account number");
            }

            @this.Set("Company name").To(companyName);
            @this.ClickField("Country");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(companyCountry);
            System.Threading.Thread.Sleep(1000);
            @this.Click(companyCountry);
            @this.Set(That.Contains, "Postcode").To(postCode);
            @this.Set("Address line 1").To(addressLine);
            @this.Set("Town/City").To(companyCity);
            @this.Set("EORI number").To(eoriNumber);

            //Adds details for if CFSP is set to ChannelPorts
            if (cfspType.Equals(CFSPConstants.ChannelPorts))
            {
                @this.AtLabel("CFSP").ClickLabel(CFSPConstants.ChannelPorts);
                @this.ExpectLabel("Using EIDR?");
                @this.AtLabel("Using EIDR?").ClickLabel(eidrType);
                @this.ExpectHeader("Authorised Company Details");
                @this.ClickField(The.Bottom, "Country");
                System.Threading.Thread.Sleep(1000);
                @this.Expect(authorisedCountry);
                System.Threading.Thread.Sleep(1000);
                @this.Click(authorisedCountry);
                @this.Set("CFSP CPC Number").To(cfspCpcNumber);
                @this.Set("Authorisation number").To(authorisationNumber);
            }
            //Adds details for if CFSP is set to OWn
            else if (cfspType.Equals(CFSPConstants.Own))
            {
                @this.AtLabel("CFSP").ClickLabel(CFSPConstants.Own);
                @this.ExpectLabel("Using EIDR?");
                @this.AtLabel("Using EIDR?").ClickLabel(eidrType);
                @this.Set("Authorisation number").To(authorisationNumber);
                @this.Set("CFSP CPC Number").To(cfspCpcNumber);
                @this.Set("Authorisation number").To(authorisationNumber);
                @this.AtLabel("SFDOnly").ClickLabel(sfdOnly);
            }
            else
            {
                @this.AtLabel("CFSP").ClickLabel(CFSPConstants.None);
                @this.ExpectNo("Using EDIR?");
                @this.ExpectNo("Authorisation number");
                @this.ExpectNo("CFSP CPC Number");
                @this.ExpectNo("Warehouse Number");
            }

            //Adds payment information if a Payment type is selected
            if (paymentType.HasAny())
            {
                @this.Set("Payment type").To(paymentType);
                @this.ExpectLabel("Deferment number");
                @this.ExpectLabel("VAT by DAN");
                @this.Set("Deferment number").To(defermentNumber);
                @this.AtLabel("VAT by Dan").ClickLabel(vatByDan);
            }
            @this.AtLabel("Representation type").ClickLabel(representationType);

            if (guarantorType == GuarantorType.Own)
            {
                @this.Set("Transit Guarantee").To(transitGuarantee);
                @this.Set("Guarantee type").To(guaranteeType);
            }
            else if (guarantorType == GuarantorType.DifferentCompany)
            {
                @this.ExpectField("Guarantor Name");
                @this.ClickField("Guarantor Name");
                System.Threading.Thread.Sleep(1000);
                @this.Expect(guarantorName);
                System.Threading.Thread.Sleep(1000);
                @this.Click(guarantorName);
            }
            else
            {
                @this.AtLabel("Guarantor Type").ClickLabel(GuarantorType.None);
                @this.ExpectNo("Guarantor Name");
                @this.ExpectNo("Transit Guarantee");
                @this.ExpectNo("Guarantee type");
            }

            if (authorisedLocations.HasAny())
            {
                @this.AtLabel("Authorised locations").Click(What.Contains, "Nothing selected");
                foreach (var authorisedLocation in authorisedLocations)
                {
                    @this.Click(authorisedLocation);
                }
            }

            //Saves the Shipment
            @this.ClickButton("Save");
        }
    }
}

