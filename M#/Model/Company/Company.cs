using MSharp;

namespace Domain
{
    class Company : EntityType
    {
        public Company()
        {
            Associate<CompanyType>("Type").Mandatory().Default("c#:CompanyType.Other").DatabaseIndex();
            InverseAssociate<CompanyShipmentType>("TransactionTypes", "Company");
            Associate<GVMSType>("GVMS").Default("NotGVMS");
            String("Customer account number").Unique().MinLength(5).Max(5);
            DefaultSort = String("Name").Mandatory();
            Associate<Country>("Country").Mandatory().DatabaseIndex();
            String("Postcode").Mandatory();
            String("Address line 1").Mandatory();
            String("Address line 2");
            String("Town/City").Name("Town").Mandatory();
            String("EORI number");
            String("Branch identifier").Max(5).MinLength(5);
            String("AEO number").Max(20);
            String("TSP").Max(20).MinLength(20);
            //Bool("CFSP").Mandatory(value: false);
            Associate<Company>("Default declarant");
            Associate<PaymentType>("Payment type");
            //String("Payment code").Max(1).MinLength(1);
            String("Deferment number").Max(7).MinLength(7).Accepts(TextPattern.IntegerText_digitsOnly);
            Bool("Representation type").Mandatory().TrueText("Direct").FalseText("Indirect").Default("true");
            String("Guarantee number");
            String("Guarantee type").MinLength(1).Max(1);
            String("TIN");
            String("PIN");
            Bool("Is default declarant").Mandatory().CalculatedFrom("ID == Settings.Current.DefaultDeclarant").TrueText("Yes").FalseText("No");

            String("Address").CalculatedFrom("new string[] { AddressLine1, AddressLine2, Town, Postcode }.Trim().ToString(\", \")");
            String("Refrence suffix").Mandatory().Max(3);

            String("AddressStreet").CalculatedFrom("new string[] { AddressLine1, AddressLine2 }.Trim().ToString(\", \")");
            // API properties
            String("Username").Unique().Accepts(TextPattern.EmailAddress);
            String("Password", 100);
            Bool("Is created from API").Mandatory().Default("false").DatabaseIndex();

            Associate<Person>("Primary contact");

            InverseAssociate<Ancillary>("Ancillaries", "Company");
            InverseAssociate<Contact>("Contacts", "Company");
            InverseAssociate<Note>("Notes", "Company");
            InverseAssociate<ContactGroup>("Contact groups", "Company");
            InverseAssociate<Product>("Products", "Company");
            InverseAssociate<Shipment>("Shipments", "Company");
            InverseAssociate<CompanyUser>("Company users", "Company");
            InverseAssociate<Charge>("Custom Charges", "Company");
            this.Archivable();

            InverseAssociate<CompanyAssociationLink>("AssociatedCompanies", "Company");

            AssociateManyToMany<AuthorisedLocation>("Authorised locations");

            InverseAssociate<CompanySpecialCPCLink>("SpecialCPCs", "Company");
            InverseAssociate<CompanyUKTraderPartnerLink>("UK traders and partners", "Company");

            Bool("Send invoices to Accounting Department").Mandatory(value: false);
            Bool("Is On Hold").Mandatory();
            Associate<ChannelPortsUser>("Placed on hold by");
            String("Department Name");
            String("Department Email");
            String("Accounting address line 1");
            String("Accounting address line 2");
            String("Accounting address line 3");
            String("Accounting Country");
            String("Accounting Postcode");
            String("Account Number");
            String("Sort Code");

            Associate<InvoiceFrequencyType>("Invoice Frequency").Default("Monthly");
            Associate<LicenseStartingMonthOption>("License fee invoicing start month");
            Associate<Charge>("Invoice Charge");
            Bool("VAT by DAN").Mandatory(value: false);

            Associate<SafetyAndSecurity>("Safety and security inbound");
            Associate<SafetyAndSecurity>("Safety and security outbound");

            ToStringExpression("GetDisplayText()");

            InverseAssociate<Deposit>("Deposits", "Company");

            Associate<GuarantorType>("GuarantorType"); // default to own in database.
            Associate<Company>("Guarantor name");

            Associate<CFSPType>("CFSP Type");
            Bool("Using EIDR").Mandatory(value: false);
            //Bool("Channelports CFSP").Mandatory(value: false); 
            String("Authorisation number");
            Bool("SFDOnly").Mandatory(value: false);

            String("Authorised Company Name");
            String("Authorised Postcode");
            String("Authorised Address Line 1");
            String("Authorised Address Line 2");
            String("Authorised Town or City");
            String("Authorised Warehouse Number");
            Associate<Country>("Authorised Country").DatabaseIndex();
            Associate<CFSPCPCNumber>("Authorised CFSPCPCNumber").DatabaseIndex();

            String("Local Officer Supervising Office");
            String("Local Officer Street");
            String("Local Officer Postcode");
            String("Local Officer City");
            String("Local Officer Warehouse Number");
            Associate<Country>("Local Officer Country Code").DatabaseIndex();

            Money("Overdraft Amount").IsCurrency(false);

        }
    }
}
