using MSharp;

namespace Modules
{
    class RoutesForm : FormModule<Domain.Route>
    {
        public RoutesForm()
        {
            HeaderText("Route Details");

            Field(x => x.UKPort).AsDropDown().DataSource("await Database.GetList<Port>(x => x.Non_UK == false)");
            Field(x => x.Non_UKPort).AsDropDown().DataSource("await Database.GetList<Port>(x => x.Non_UK == true)");
            Field(x => x.IsManual)
                .Mandatory()
                .Label("Manual")
                .AsRadioButtons(Arrange.Horizontal);

            MasterDetail(x => x.RouteItineraryCountry, s =>
            {
                s.Orientation(Arrange.Vertical);

                s.Field(x => x.Country).SourceCriteria("!item.IsDeactivated").AsDropDown().Label("Country").Mandatory()
                .LabelCssClass("country-label-default");

                s.Button("Add another").CssClass("update-country-label").OnClick(x => x.AddMasterDetailRow());

            }).MinCardinality(0).NoLabel().SourceCriteria("item.RouteItinerary.HasDefault");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.CSharp("await info.Item.SetDefaultItinerariy();");
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });
        }
    }
}