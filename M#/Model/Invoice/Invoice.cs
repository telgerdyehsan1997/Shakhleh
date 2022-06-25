using MSharp;

namespace Domain
{
    class Invoice : EntityType
    {
        public Invoice()
        {
            Associate<Company>("Company").Mandatory();
            Int("InvoiceNumber").AutoNumber().Min(10000).Mandatory();
            Associate<Charge>("Charge").Mandatory();
            Associate<InvoiceType>("Type").Mandatory();
            DateTime("Generate at").Mandatory();
            DateTime("Print date").Mandatory();
            DateTime("Due date").Mandatory();
            DateTime("Invoice period start date").Mandatory();
            DateTime("Invoice period end date").Mandatory();
            SecureFile("Invoice excel file");
            SecureFile("Invoice pdf file");
            Money("Total").Mandatory();
            Money("Total Net").Mandatory();
            Money("Total Discount").Mandatory().Default("0");
            Money("Total Vat").Mandatory();
            Bool("Is Paid").Mandatory();
            Associate<InvoiceStatus>("Status").Mandatory().Default(cs("InvoiceStatus.InProgress"));

            Int("Invoice Year").Mandatory();
            Int("Invoice Month");

            Int("Number of Free consignments").Mandatory().Default("0");


            InverseAssociate<TransactionInvoiceRow>("Transactions", "Invoice");
            InverseAssociate<ChargeInvoiceRow>("Charges", "Invoice");

            UniqueCombination(new[] { "Company", "Invoice Year", "Invoice Month", "Type" });

        }
    }
}