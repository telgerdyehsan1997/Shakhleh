using MSharp;

namespace Modules
{
    class InvoiceList : MFABaseList<Domain.Invoice>
    {
        public InvoiceList()
        {
            
            HeaderText("Invoices");
            this.AddDependency<Domain.IInvoiceService>();
            this.AddDependency<Domain.IExchequerService>();
            this.AddDependency<Domain.ILookupService>();
            DataSource("await InvoiceService.GetInvoices(info.InvoiceYear, info.Month?.MonthNumber, info.Type, info.Company)");


            Search(x => x.InvoiceYear)
                .Label("Year")
                .AsDropDown();                ;
            CustomSearch("Month")
                .Label("Month")
                .ViewModelType("LicenseStartingMonthOption")
                .ViewModelName("Month")
                .AsDropDown()
                .DataSource("await Database.GetList<LicenseStartingMonthOption>().OrderBy(x => x.MonthNumber)");
            Search(x => x.Company).Label("Customer")
                .AsAutoComplete()
                .DataSource("LookupService.GetActiveCompanyList()");
            Search(x => x.Type).Label("Invoice Type")
                .AsRadioButtons(Arrange.Horizontal)
                .DisplayExpression("item.DisplayName")
                .DataSource("Database.GetList<InvoiceType>()");

            Search(x => x.Status)
                .Label("Status")
                .AsRadioButtons(Arrange.Horizontal)
                .DisplayExpression("item.DisplayName")
                .DataSource("Database.GetList<InvoiceStatus>()");

            SearchButton("Search")
                .Icon(FA.Search)
                .IsDefault(true)
                .OnClick(x => x.ReturnView());

            Column(x => x.InvoiceMonth)
                .DisplayExpression("@(item.InvoiceMonth.HasValue ? System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.InvoiceMonth.Value) : \"\")");
            Column(x => x.GenerateAt).LabelText("Invoice Date")
                .DisplayExpression("@item.GenerateAt.ToShortDateString()");
            Column(x => x.Type.DisplayName).LabelText("Invoice Type");
            Column(x => x.Company).LabelText("Customer");
            Column(x => x.InvoiceNumber);
            Column(x => x.TotalNet).LabelText("Net Amount");
            Column(x => x.TotalVat).LabelText("VAT Amount");
            Column(x => x.Total).LabelText("Total Amount");
            Column(x => x.DueDate)
                .DisplayExpression("@item.DueDate.ToShortDateString()");
            Column(x => x.Status.DisplayName).LabelText("Status");
            Column(x => x.InvoiceExcelFile);
            Column(x => x.InvoicePdfFile);

            Button("Send License Invoices")
                .CssClass("float-right")
                .OnClick(x =>
                {
                    x.CSharp(@"var items = info.Items.Select(x => x.Item).Where(x => x.Type == InvoiceType.Charge && x.Status != InvoiceStatus.SentToExchequer);
                    await ExchequerService.SendInvoices(items);");
                    x.RefreshPage();
                });

            Button("Send Additional Consignment Invoices")
                .CssClass("float-right")
                .OnClick(x =>
                {
                    x.CSharp(@"var items = info.Items.Select(x => x.Item).Where(x => x.Type == InvoiceType.Transaction && x.Status != InvoiceStatus.SentToExchequer);
                    await ExchequerService.SendInvoices(items);");
                    x.RefreshPage();
                });

        }
    }
}