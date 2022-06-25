using MSharp;

namespace Share.Shipment.ShipmentView
{
    class TaxAmountPage : SubPage<ShipmentViewPage>
    {
        public TaxAmountPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Layout(Layouts.FrontEndModal);
            Add<Modules.TaxAmountView>();
        }
    }
}