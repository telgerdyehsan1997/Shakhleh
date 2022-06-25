using MSharp;

namespace Modules
{
    class CompanyAccountingForm : MFABaseForm<Domain.Company>
    {
        public CompanyAccountingForm()
        {

            HeaderText("Accounting Information");
            DataSource("info.Company");

            Field(x => x.SendInvoicesToAccountingDepartment)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons);

            Field(x => x.DepartmentName);
            Field(x => x.DepartmentEmail);
            Field(x => x.AccountingAddressLine1).Label("Address Line 1");
            Field(x => x.AccountingAddressLine2).Label("Address Line 2");
            Field(x => x.AccountingAddressLine3).Label("Address Line 3");
            Field(x => x.AccountingCountry).Label("Country");
            Field(x => x.AccountingPostcode).Label("PostCode");
            Field(x => x.PlacedOnHoldBy).Readonly();
            Field(x => x.InvoiceFrequency).Mandatory().AsRadioButtons(Arrange.Horizontal)
                .ReloadOnChange();
            Field(x => x.LicenseFeeInvoicingStartMonth)
                .VisibleIf("info.InvoiceFrequency == InvoiceFrequencyType.Yearly")
                .AsDropDown()
                .DataSource("await Database.Of<LicenseStartingMonthOption>().OrderBy(x => x.MonthNumber).GetList()");

            Field(x => x.InvoiceCharge)
                .Label("License")
                .CustomInitializer(@"            
            info.InvoiceCharge_Options.Clear();
            info.InvoiceCharge_Options.Add(new EmptyListItem());
            info.InvoiceCharge_Options.AddRange(await Database.GetList<Charge>(x => x.Name != ""Custom"" && x.IsDefault));
            info.InvoiceCharge_Options.Add(await Database.FirstOrDefault<Charge>(x => x.Name == ""Custom"" && x.IsDefault)); 
            if (Request.IsGet() && info.Item.InvoiceCharge?.IsDefault == false)
            {
                info.InvoiceCharge = await Database.FirstOrDefault<Charge>(x => x.Name == ""Custom"" && x.IsDefault);
                info.CustomInvoiceCharge = info.Item.InvoiceCharge;
            }")
                .ReloadOnChange();

            CustomField("Custom Invoice Charge")
                .PropertyName("CustomInvoiceCharge")
                .PropertyType("Charge")
                .Label("Select Custom License")
                .AsDropDown()
                .VisibleIf("info.InvoiceCharge?.Name == \"Custom\"")
                .DataSource("await Database.GetList<Charge>(x => !x.IsDefault && x.CompanyId == info.Company)");


            Field(x => x.AccountNumber);
            Field(x => x.SortCode);
            Field(x => x.OverdraftAmount);


            Button("Activate Account").VisibleIf("info.Company.IsOnHold")
                .ConfirmQuestion("Are you sure you want to activate this account?")
                .OnClick(x =>
                 {
                     x.CSharp(@"info.Item.IsOnHold = false;
                     info.Item.PlacedOnHoldBy = null;");
                     x.SaveInDatabase();
                     x.RefreshPage();
                 });

            Button("Place Account On Hold").VisibleIf("!info.Company.IsOnHold")
                .ConfirmQuestion("Are you sure you want to place this account on hold?")
                .OnClick(x =>
                {
                    x.CSharp(@"info.Item.IsOnHold = true;
                    info.Item.PlacedOnHoldBy = ChannelPortsUser;");
                    x.SaveInDatabase();
                    x.RefreshPage();
                });

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.If("info.InvoiceCharge?.Name == \"Custom\"")
                    .CSharp(@"info.InvoiceCharge = info.CustomInvoiceCharge;");

                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.RefreshPage();
            });

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}