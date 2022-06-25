using Domain;
using MSharp;
using System.Xml.Linq;
using Olive;

namespace Modules
{
    class CompanyPendingDepositForm : FormModule<Domain.Deposit>
    {
        public CompanyPendingDepositForm()
        {
            HeaderText("Pending Transaction Deposit Details")
                .RequestParam("deposit");

            AutoSet(x => x.TransactionType).Value("TransactionType.Pending");

            Field(x => x.DateAdded)
                .AsDatePicker();

            Field(x => x.Value)
                .Label("Withdrawal");


            Field(x => x.Consignment)
                .Label("Tracking number")
                .SourceCriteria("item.Shipment.Company == info.Company")
                .Control(ControlType.AutoComplete);

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {             
                x.SaveInDatabase();             
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });

            ViewModelProperty<Company>("Company").FromRequestParam("company");
        }
    }
}
