using MSharp;

namespace Share.Commodities.Import
{
    class ErrorLogPage : SubPage<ImportPage>
    {
        public ErrorLogPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.CommodityImportErrorList>();
        }
    }
}