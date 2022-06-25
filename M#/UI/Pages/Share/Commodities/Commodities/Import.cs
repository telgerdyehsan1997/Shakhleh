using MSharp;

namespace Share.Commodities
{
    class ImportPage : SubPage<CommoditiesPage>
    {
        public ImportPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.CommodityImportForm>();

        }
    }
}