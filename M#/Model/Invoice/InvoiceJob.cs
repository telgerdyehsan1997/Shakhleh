using MSharp;

namespace Domain
{
    class InvoiceJob : EntityType
    {
        public InvoiceJob()
        {
            DateTime("Creation Date").Mandatory().Default(cs("LocalTime.Now"));
            DateTime("Invoice To Date").Mandatory(); 
            Associate<InvoiceJobStatus>("Status").Mandatory().Default(cs("InvoiceJobStatus.NotStarted"));
            Associate<InvoiceType>("Invoice Type").Mandatory();
            Int("Indicator Month").Mandatory();

            UniqueCombination(new[] { "Indicator Month", "Invoice Type" });

        }
    }
}