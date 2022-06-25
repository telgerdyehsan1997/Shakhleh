using Domain;
using MSharp;

namespace Modules
{
    class TaxAmountView : ViewModule<Domain.Consignment>
    {
        public TaxAmountView()
        {

            HeaderText("View Tax Amount");

            CustomField("Deferment Number").LabelText("Deferment Number").DisplayExpression(cs("(await item.GetSentDeferment())"));
            CustomField("VAT Paid").LabelText("VAT Paid").DisplayExpression(cs("item.TotalVatPaid"));
            CustomField("Duty Paid").LabelText("Duty Paid").DisplayExpression(cs("item.TotalDuty"));
            CustomField("Other charges").LabelText("Other charges").DisplayExpression(cs("item.TotalOther"));

            DataSource("info.Consignment");
            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("item");
        }
    }
}