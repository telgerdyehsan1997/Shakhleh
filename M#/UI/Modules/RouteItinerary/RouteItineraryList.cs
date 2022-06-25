using MSharp;

namespace Modules
{
    class RouteItineraryList : BaseListModule<Domain.RouteItinerary>
    {
        public RouteItineraryList()
        {
            HeaderText("Itineraries").DataSource("await info.Route.RouteItinerary.GetList()").SourceCriteria("!item.HasDefault");

            Search(x => x.DestinationCountry);
            Search(x => x.IsDeactivated).Control(ControlType.HorizontalRadioButtons).Label("Status").DefaultValueExpression("false");
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.DestinationCountry);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.Routes.RouteItineraries.EnterPage>().Send("item", "item.ID").Pass("route").SendReturnUrl());

            this.ArchiveButtonColumn("Itinerary");

            Button("New Itinerary").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.Routes.RouteItineraries.EnterPage>().Pass("route").SendReturnUrl());

            ViewModelProperty<Domain.Route>("Route").FromRequestParam("route");
        }
    }
}
