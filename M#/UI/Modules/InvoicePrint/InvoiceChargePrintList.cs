using MSharp;
using System;
using System.Collections.Generic;

namespace Modules
{
    class InvoiceChargePrintList : ListModule<Domain.Invoice>
    {
        public InvoiceChargePrintList()
        {
            SourceCriteria("item.ID == info.Invoice");
            VisibleIf("info.Invoice.Type == InvoiceType.Charge");
            ShowFooterRow();

            Column(x => x.Charge.Name)
                .LabelText("Details of Charge/Type of Supply: Service");

            Column(x => x.Total)
                .LabelText("Amount")
                .DisplayExpression(@"@(string.Format(item.Charge.CurrencyId == ChargeCurrencyOption.Euro ? System.Globalization.CultureInfo.GetCultureInfo(""en-ie"") : System.Globalization.CultureInfo.GetCultureInfo(""en-GB""), ""{0:c}"", item.Total))")
                .FooterFormula(AggregateFormula.Sum);


            CustomColumn("Tax Rate")
                .LabelText("Tax Rate")
                .DisplayExpression(@"@(item.Company.Country.Code == ""GB"" ? ""S"" : ""Z"")");

            ViewModelProperty<Domain.Invoice>("Invoice").FromRequestParam("item");
        }
    }
}