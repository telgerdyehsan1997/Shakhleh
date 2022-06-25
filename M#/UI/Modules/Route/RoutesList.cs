using MSharp;

namespace Modules
{
    class RoutesList : BaseListModule<Domain.Route>
    {
        public RoutesList()
        {
            HeaderText("Routes");

            Search(GeneralSearch.AllFields).Label("Find")
                .MemoryFilterCode(@"if (!info.FullSearch.IsEmpty())
            {
                result = result.Where(x => x.UKPort.PortName.Contains(info.FullSearch) || x.Non_UKPort.PortName.Contains(info.FullSearch));
            }
            ");
            Search(x => x.IsDeactivated).Control(ControlType.HorizontalRadioButtons).Label("Status").DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.UKPort);
            Column(x => x.Non_UKPort);
            Column(x => x.IsManual).LabelText("Manual");
            LinkColumn("@await item.RouteItinerary.Where(x=>!x.HasDefault).Count()").HeaderText("Itineraries")
                .OnClick(x => x.Go<Admin.Settings.Routes.RouteItineraryPage>().Send("route", "item.ID").SendReturnUrl());



            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.Routes.EnterPage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Route");

            Button("New Route").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.Routes.EnterPage>().SendReturnUrl());
        }
    }
}