using MSharp;

namespace Modules
{
    class CurrencyList : BaseListModule<Domain.Currency>
    {
        public CurrencyList() : base()
        {
            HeaderText("Currencies");
            EmptyMarkup("There are no Currencies to display");

            this.ArchiveSearch();
            SearchButton("Search").IsDefault().Icon(FA.Search)
                .OnClick(x => x.ReturnView());

            Column(x => x.Name).LabelText("Currency");
            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.Currencies.EnterPage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Currency");

            Button("New Currency").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.Currencies.EnterPage>().SendReturnUrl());
        }
    }
}
