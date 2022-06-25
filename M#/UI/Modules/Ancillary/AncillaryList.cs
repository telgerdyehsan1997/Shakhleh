using MSharp;

namespace Modules
{
    class AncillaryList : BaseListModule<Domain.Ancillary>
    {
        public AncillaryList()
        {
            HeaderText("Ancillaries").SourceCriteria("item.Company == null")
                .SortingStatement("Country.Code");
            EmptyMarkup("There are no Ancillaries to display");

            Search(x => x.Country.Code).Label("Find")
                .MemoryFilterCode(@"if (info.Country_Code.HasValue())
            {
                result = result.Where(item => item.Country.Code.Contains(info.Country_Code.Trim(), false));
            }");

            SearchButton("Search")
                .Icon(FA.Search)
                .OnClick(x => x.Reload());

            Column(x => x.Country.Code).LabelText("Country");
            Column(x => x.FreightChargePerTonne);
            Column(x => x.FullLoadFreightCharge);
            Column(x => x.ValueForVAT);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.Ancillaries.EnterPage>().Send("item", "item.ID").SendReturnUrl());
        }
    }
}