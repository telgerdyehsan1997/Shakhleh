using MSharp;

namespace Modules
{
    class CarrierList : BaseListModule<Domain.Carrier>
    {
        public CarrierList()
        {
            HeaderText("Carriers")
                .UseDatabasePaging()
                .DataSource("await (await FilterCarrier(info)).GetList()");

            Search(x => x.Name).Label("Carrier name")
                .MemoryFilterCode("//remove from here");

            this.ArchiveSearch().MemoryFilterCode("//remove from here");

            SearchButton("Search")
                .IsDefault()
                .Icon(FA.Search)
                .OnClick(x =>
                {
                    x.ReturnView();
                });

            Column(x => x.Name);

            Column(x => x.Address);

            Column(x => x.Country.Code).LabelText("Country");

            Column(x => x.EORINumber).LabelText("EORINumber");


            ButtonColumn("Edit")
                .HeaderText("Edit")
                .GridColumnCssClass("actions")
                .Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Carrier.CarrierEnterPage>().Send("item", "item.ID")
                .SendReturnUrl())
                .ColumnVisibleIf(AppRole.SuperAdmin);

            this.ArchiveButtonColumn("Carrier")
                .ColumnVisibleIf(AppRole.SuperAdmin);

            Button("New Carrier").Icon(FA.Plus)
               .OnClick(x => x.Go<Admin.Carrier.CarrierEnterPage>().SendReturnUrl());
        }
    }
}
