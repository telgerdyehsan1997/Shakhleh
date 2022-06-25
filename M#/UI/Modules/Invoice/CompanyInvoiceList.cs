using MSharp;

namespace Modules
{
    class CompanyInvoiceList : ListModule<Domain.Invoice>
    {
        public CompanyInvoiceList()
        {
            
            HeaderText("Invoices");
            this.AddDependency<Domain.IInvoiceService>();
            DataSource("InvoiceService.GetInvoices(info.Company)");
            PageSize(10);

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");

            DataSource("await InvoiceService.GetInvoices(info.InvoiceYear, info.Month?.MonthNumber, info.Type, info.Company)");

            Search(x => x.InvoiceYear)
                .Label("Year")
                .AsDropDown(); ;
            CustomSearch("Month")
                .Label("Month")
                .ViewModelType("LicenseStartingMonthOption")
                .ViewModelName("Month")
                .AsDropDown()
                .DataSource("await Database.GetList<LicenseStartingMonthOption>().OrderBy(x => x.MonthNumber)");
            Search(x => x.Type).Label("Invoice Type")
                .AsRadioButtons(Arrange.Horizontal)
                .DisplayExpression("item.DisplayName")
                .DataSource("Database.GetList<InvoiceType>()");
            SearchButton("Search")
                .Icon(FA.Search)
                .IsDefault()
                .OnClick(x => x.ReturnView());

            Column(x => x.InvoiceNumber);
            Column(x => x.InvoiceMonth)
                .DisplayExpression("@(item.InvoiceMonth.HasValue ? System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(item.InvoiceMonth.Value) : \"\")");
            Column(x => x.Type)
                .LabelText("Invoice Type");
            Column(x => x.Total)
                .LabelText("Total Amount");
            Column(x => x.InvoiceExcelFile);
            Column(x => x.InvoicePdfFile);
            
        }
    }
}