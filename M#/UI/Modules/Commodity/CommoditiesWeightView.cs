using MSharp;

namespace Modules
{
    class CommoditiesWeightView : ViewModule<Domain.Consignment>
    {
        public CommoditiesWeightView()
        {
            HeaderText("@info.Consignment.ConsignmentNumber - Commodities [#BUTTONS(Import)#] [#BUTTONS(NewCommodity)#] [#BUTTONS(Back)#] ");

            DataSource("info.Consignment");
            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
            ViewModelProperty<bool>("IsEditPage").RetainInPost();

            Field(x => x.TotalGrossWeight).LabelText("Consignment total gross weight").DisplayFormat("{0:#,#.##} kg");
            Field(x => x.TotalNetWeight).LabelText("Consignment total net weight").DisplayFormat("{0:#,#.##} kg");
            Field(x => x.TotalValue).LabelText("Consignment total value").DisplayFormat("{0:#,0.##}");
            Field(x => x.TotalPackages).LabelText("Consignment total packages").DisplayFormat("{0:#,0}");

            Button("Import").CssClass("float-right")
                .VisibleIf("GetUser() is ChannelPortsUser")
                .OnClick(x => x.PopUp<Share.Commodities.ImportPage>()
                .Send("consignment","info.Consignment.ID.ToString()"));

            Button("New Commodity").IsDefault().Icon(FA.Plus).CssClass("float-right")
                .VisibleIf("await info.Consignment.CanAddCommodity(GetUser()) && (info.Consignment.IsHaveCFSP() ? (await info.Consignment.Commodities.Count()) < 1 : true)")
                .OnClick(x =>
                {
                    x.Go<Share.Commodities.CommodiyEnterPage>().Pass("consignment").SendReturnUrl();
                });

            Button("Back").CssClass("float-right")
               .OnClick(x =>
               {
                   x.ReturnToPreviousPage();
               });

            OnBound_GET("check if page is editiable")
                .Code(@"info.IsEditPage = ControllerContext.RouteData.Values[""controller""].ToString() != ""CommoditiesView"";");
        }
    }
}