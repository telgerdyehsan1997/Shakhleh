using MSharp;

namespace Modules
{
    class PaymentTypeList : BaseListModule<Domain.PaymentType>
    {
        public PaymentTypeList()
        {
            HeaderText("Payment Types");

            Search(GeneralSearch.AllFields).Label("Find");
            Search(x => x.IsDeactivated).Label("Status").Control(ControlType.HorizontalRadioButtons).DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.Code);
            Column(x => x.Description);

            ButtonColumn("Edit")
                .HeaderText("Edit")
                .GridColumnCssClass("actions")
                .Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.PaymentType.EnterPage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Payment Type");

            Button("Payment Type")
                .Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.PaymentType.EnterPage>().SendReturnUrl());
        }
    }
}