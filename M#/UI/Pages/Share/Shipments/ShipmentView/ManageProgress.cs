using MSharp;
using Share.Shipment;

namespace Share.Shipment.ShipmentView
{
    class ManageProgressPage : SubPage<ShipmentViewPage>
    {
        public ManageProgressPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.UpdateConsignmentProgressForm>();
            Add<Modules.ConsignmentProgressHistoryList>();
        }
    }
}