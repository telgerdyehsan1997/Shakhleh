using MSharp;

namespace Modules
{
    class CommoditiesDerermentNumberWarningView : FormModule<Domain.Consignment>
    {
        public CommoditiesDerermentNumberWarningView()
        {
            HeaderText("Your deposit does not have sufficient balance to process this shipment");
            Header("<p> Please arrange to top up your deposit so this shipment can be processed or in the event of a problem contact CustomsPro </p>");
            Button("Ok").OnClick(x => x.CloseModal());
           
        }
    }
}