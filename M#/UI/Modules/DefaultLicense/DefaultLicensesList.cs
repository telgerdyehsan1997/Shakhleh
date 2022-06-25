using MSharp;

namespace Modules
{
    class DefaultLicensesList : MFABaseList<Domain.Charge>
    {
        public DefaultLicensesList()
        {
            HeaderText("Default licenses");

            DataSource("await Charge.GetChargeList()");

            this.ArchiveSearch().DefaultValueExpression("false");
            SearchButton("Search")
                .OnClick(x => x.ReturnView());
            
            Column(x => x.Name);
            Column(x => x.LicenseFee);
            Column(x => x.IsYearly)
                .LabelText("Is Charged Yearly");
            Column(x => x.FreeConsignments);
            Column(x => x.PricePerAdditionalConsignment);
            Column(x => x.PricePerCommodity);
            Column(x => x.Currency.Name)
                .LabelText("Currency");
            ButtonColumn("Edit").HeaderText("Actions").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Accounting.DefaultLicenses.EnterPage>()
                .Send("item", "item.ID")
                .SendReturnUrl());
            this.ArchiveButtonColumn("Default License");

            Button("New Default license").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Accounting.DefaultLicenses.EnterPage>()
                .SendReturnUrl());
        }
    }
}