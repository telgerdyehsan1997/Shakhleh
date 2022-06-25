using Domain;
using MSharp;

namespace Modules
{
    class ExchangeRateList : BaseListModule<Domain.ExchangeRate>
    {
        public ExchangeRateList()
        {
            HeaderText("Exchange rates @info.ExchangeFile.Name").
                    DataSource("await ExchangeRate.GetExchangeRateList(info.ExchangeFile)");


            Search(GeneralSearch.AllFields).Label("Find:");
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Country_Territory);
            Column(x => x.Currency);
            Column(x => x.Rate);
            ButtonColumn("Update Rate").HeaderText("Update").GridColumnCssClass("actions")
             .OnClick(x =>
             {
                 x.PopUp<Admin.Settings.ExchangeRateEnterPage>().Send("item", "item.ID").SendReturnUrl();
             });

            ViewModelProperty<ExchangeRateFile>("ExchangeFile").FromRequestParam("item");

        }
    }
}