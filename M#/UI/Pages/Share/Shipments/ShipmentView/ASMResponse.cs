using MSharp;
using Share.Shipment;

namespace Share.Shipment.ShipmentView
{
    class ASMResponsePage : SubPage<ShipmentViewPage>
    {
        public ASMResponsePage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.ProgressASMResponseView>();
        }
    }
}