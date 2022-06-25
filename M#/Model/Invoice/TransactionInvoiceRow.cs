using MSharp;

namespace Domain
{
    class TransactionInvoiceRow : EntityType
    {
        public TransactionInvoiceRow()
        {
            Associate<Invoice>("Invoice").Mandatory();
            String("Shipment Tracking Number").Mandatory();
            String("Customer Reference").Mandatory(); 
            String("Vehicle Number").Mandatory(); 
            Int("Number of consignments").Mandatory();
            Money("Total Value").Mandatory();
            String("TaxRate").Mandatory(); 
            Money("Net Value").Mandatory();
            Money("Vat Value").Mandatory();

            ToStringExpression(@"$""{ShipmentTrackingNumber}-{CustomerReference}-{VehicleNumber}-{NumberOfConsignments}""");
        }
    }
}