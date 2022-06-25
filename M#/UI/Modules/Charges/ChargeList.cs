using Domain;
using MSharp;

namespace Modules
{
    class ChargeList : MFABaseList<Domain.Charge>
    {
        public ChargeList()
        {
            HeaderText("Custom Licenses");
            DataSource("await info.Company.CustomCharges.GetList()");
            EmptyMarkup("There are no charges to display");
            Button("New Custom Licenses").IsDefault().Icon(FA.Plus)
               .OnClick(x => x.Go<Admin.Company.Charge.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            Search(x => x.IsDeactivated).Control(ControlType.HorizontalRadioButtons).Label("Status").DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());
           
            Column(x => x.ValidFrom);
            Column(x => x.Name);
            Column(x => x.LicenseFee);
            Column(x => x.IsYearly)
                .LabelText("Is Charged Yearly");
            Column(x => x.FreeConsignments).LabelText("Free Consignments");
            Column(x => x.PricePerAdditionalConsignment).LabelText("Price Per Additional Consignment");
            Column(x => x.PricePerCommodity).LabelText("Price Per Commodity");
            Column(x => x.Currency);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Company.Charge.EnterPage>().Send("item", "item.ID").Send("company", "info.Company.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Charge");

            ViewModelProperty<Company>("Company").FromRequestParam("company");

        }
    }
}