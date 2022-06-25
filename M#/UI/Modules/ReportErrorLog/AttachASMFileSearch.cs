using Domain;
using MSharp;

namespace Modules
{
    class AttachASMFileSearch : ListModule<Domain.ASMFileConsignmentViewModel>
    {
        public AttachASMFileSearch()
        {
            HeaderText("@info.ReportErrorLog.FileName Actions");
            DataSource("Enumerable.Empty<ASMFileConsignmentViewModel>()");

            EmptyMarkup("There are no consignments to display.");

            Search(x => x.Type)
                .DataSource("Database.GetList<ShipmentBaseType>()")
                .AsRadioButtons(Arrange.Horizontal);

            Search(x => x.UCR)
                .Label("Declaration Unique Consignment Reference (DUCR)/Consignment no.");

            Search(x => x.LRN)
                .Label("LRN/Tracking number");

            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            ViewModelProperty<Domain.ReportErrorLog>("ReportErrorLog").FromRequestParam("item");

            OnPostBound("Set Type Options")
                .Code(@"
            info.Type_Options.Clear();
            info.Type_Options.AddRange(await Database.GetList<ShipmentBaseType>());
            info.Type_Options.SetSelected(""EAD"");
            ");

        }
    }
}