using MSharp;

namespace Share.Shipment.ShipmentView
{
    class ConsignmentDocumentsPage : SubPage<ShipmentViewPage>
    {
        public ConsignmentDocumentsPage()
        {
            Add<Modules.ConsignmentDocumentForm>();
            Add<Modules.ConsignmentDocumentList>();
        }
    }
}