using MSharp;
using Share.Shipment;

namespace Share.Shipment.ShipmentView
{
    class EditVehicleNumberPage : SubPage<ShipmentViewPage>
    {
        public EditVehicleNumberPage()
        {
            Roles(AppRole.Admin, AppRole.Customer);
            Layout(Layouts.FrontEndModal);
            Add<Modules.ShipmentEditVehicleForm>();
        }
    }
}