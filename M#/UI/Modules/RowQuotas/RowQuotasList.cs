using MSharp;

namespace Modules
{
    class RowQuotasList : BaseListModule<Domain.RowQuota>
    {
        public RowQuotasList() : base()
        {
            HeaderText("ROW Quota");

            Search(GeneralSearch.AllFields).Label("Find");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());


            Column(x => x.CommodityCode);
            Column(x => x.QuotaNumber);
            Column(x => x.Preference);
            Column(x => x.Countries);
          
        }
    }
}