using MSharp;

namespace Modules
{
    class UNCodesList : BaseListModule<Domain.UNCode>
    {
        public UNCodesList() : base()
        {
            HeaderText("UN Codes");

            RootCssClass("scrollable-list");

            SortingStatement("UNNo");

            Search(x => x.UNNo).Label("Find")
            .MemoryFilterCode(@"if (info.UNNo.HasValue())
            result = result.Where(c => c.UNNo.StartsWith(info.UNNo.Trim(), false));");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.UNNo);
            Column(x => x.NameAndDescription);
            Column(x => x.NomEtDescription);
            Column(x => x.Class);
            Column(x => x.ClassificationCode);
            Column(x => x.PackingGroup);
            Column(x => x.Labels);
            Column(x => x.SpecialProvisions);
            Column(x => x.LimitedAndExceptedQuantities3_4);
            Column(x => x.LimitedAndExceptedQuantities3_5);
            Column(x => x.PackingInstructions);
            Column(x => x.SpecialPackingProvisions);
            Column(x => x.MixedPackingProvisions);
            Column(x => x.Instructions);
            Column(x => x.TankCode);
            Column(x => x.VehicleForTankCarriage);
            Column(x => x.TransportCategory_TunnelRestrictionCode);
            Column(x => x.Packages);
            Column(x => x.Bulk);
            Column(x => x.Loading_UnloadingAndHandling);
            Column(x => x.Operation);
            Column(x => x.HazardIdentificationNo);
          
        }
    }
}