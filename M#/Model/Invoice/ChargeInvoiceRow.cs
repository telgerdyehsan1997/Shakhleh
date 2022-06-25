using MSharp;

namespace Domain
{
    class ChargeInvoiceRow : EntityType
    {
        public ChargeInvoiceRow()
        {
            Associate<Invoice>("Invoice").Mandatory();
           
            String("TaxRate").Mandatory();
            Money("Total Value").Mandatory();
            Money("Net Value").Mandatory();
            Money("Vat Value").Mandatory();
            
        }
    }
}