using MSharp;
using System;
using System.Collections.Generic;

namespace Modules
{
    class InvoiceTransactionPrintList : ListModule<Domain.TransactionInvoiceRow>
    {
        public InvoiceTransactionPrintList()
        {
            SourceCriteria("item.InvoiceId == info.Invoice");
            VisibleIf("info.Invoice.Type == InvoiceType.Transaction");
            ShowFooterRow();

            CustomColumn("Details")
                .LabelText("Details of Charge/Type of Supply: Service")
                .DisplayExpression(@"@($""{item.ShipmentTrackingNumber} - {item.CustomerReference} - {item.VehicleNumber} - {item.NumberOfConsignments}"")");

            Column(x => x.NetValue)
                .LabelText("Amount")
                .FooterFormula(AggregateFormula.Sum);

            CustomColumn("Tax Rate")
                .LabelText("Tax Rate")
                .DisplayExpression(@"@(item.Invoice.Company.Country.Code == ""GB"" ? ""S"" : ""Z"")");

            ViewModelProperty<Domain.Invoice>("Invoice").FromRequestParam("item");
        }
    }
}