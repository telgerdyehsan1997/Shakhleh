using MSharp;

namespace Modules
{
    class RouteItineraryForm : FormModule<Domain.RouteItinerary>
    {
        public RouteItineraryForm()
        {
            HeaderText("Itinerary Details");

            Field(x => x.DestinationCountry).Mandatory().SourceCriteria("!item.IsDeactivated").AsDropDown();
            Field(x => x.UKCountry).Label("Country 1").Readonly();
            Field(x => x.NonUKCountry).Label("Country 2").Readonly();

            MasterDetail(x => x.RouteItineraryCountry, s =>
            {
                s.Orientation(Arrange.Vertical);

                s.Field(x => x.Country)
                .SourceCriteria("!item.IsDeactivated")
                .Label("Country")
                .LabelCssClass("country-label")
                .AsDropDown();

                s.AutoSet(x => x.Route).FromRequestParam("route");

                s.Button("Add another").CssClass("update-country-label").OnClick(x => x.AddMasterDetailRow());
            }).NoLabel();

            AutoSet(x => x.Route).FromRequestParam("route");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });

            OnPostBound("update details")
                .Code(@"
                        info.Item.UKCountry = info.Route.UKPort.Country;
                        info.Item.NonUKCountry = info.Route.Non_UKPort.Country;
                        ");
        }
    }
}