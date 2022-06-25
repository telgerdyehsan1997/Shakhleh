using MSharp;

namespace Modules
{
    class CompanyCPCList : BaseListModule<Domain.CompanySpecialCPCLink>
    {
        public CompanyCPCList()
        {

            HeaderText("Special CPCs")
                .DataSource(@"await info.Company.SpecialCPCs.GetList()");


            Search(GeneralSearch.AllFields).Label("Find");
            Search(x => x.IsDeactivated).Label("Status").Control(ControlType.HorizontalRadioButtons).DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.CPC.Number);
            Column(x => x.CPC.CPCDescription);


            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
               .OnClick(x => x.Go<Admin.Company.CompanyCPC.EnterPage>().Send("item", "item.ID").Pass("company").SendReturnUrl());

            this.ArchiveButtonColumn("Archive");

            Button("New Special CPC").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Company.CompanyCPC.EnterPage>().Pass("company").SendReturnUrl());

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}