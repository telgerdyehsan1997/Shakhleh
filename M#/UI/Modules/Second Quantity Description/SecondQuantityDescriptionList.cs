using MSharp;

namespace Modules
{
    class SecondQuantityDescriptionsList : BaseListModule<Domain.SecondQuantityDescription>
    {
        public SecondQuantityDescriptionsList()
        {
            HeaderText("Second Quantity Descriptions");
            EmptyMarkup("There are no Second Quantity Descriptions to display");

            this.ArchiveSearch();
            SearchButton("Search").IsDefault().Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.QuantityCode);
            Column(x => x.Description);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.SecondQuantityDescriptions.EnterPage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Second Quantity Description");

            Button("New Second Quantity Description").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.SecondQuantityDescriptions.EnterPage>().SendReturnUrl());
        }
    }
}