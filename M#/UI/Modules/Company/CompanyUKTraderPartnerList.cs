using MSharp;

namespace Modules
{
    class CompanyUKTraderPartnerList : BaseListModule<Domain.CompanyUKTraderPartnerLink>
    {
        public CompanyUKTraderPartnerList()
        {
            PageSize(10);
            HeaderText("UK Traders/ Partners")
                .DataSource(@"await info.Company.UKTradersAndPartners.GetList()");
            
            Search(x => x.UKTraderPartner).Label("Name");
            Search(x => x.Type).AsRadioButtons(Arrange.Horizontal).DisplayExpression("item.Display");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.UKTraderPartner).LabelText("Name");
            Column(x => x.Type).DisplayExpression("@(item.Type.Display)");

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}