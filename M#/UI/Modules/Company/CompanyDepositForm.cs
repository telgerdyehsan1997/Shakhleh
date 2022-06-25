using Domain;
using MSharp;
using System.Xml.Linq;
using Olive;

namespace Modules
{
    class CompanyDepositForm : FormModule<Domain.Deposit>
    {
        public CompanyDepositForm()
        {
            HeaderText("Deposit Details")
                .RequestParam("deposit");

            Field(x => x.TransactionType)
                .AsRadioButtons(Arrange.Horizontal)
                .SourceCriteria("item != TransactionType.Pending")
                .ReloadOnChange();

            Field(x => x.DateAdded)
                .AsDatePicker();

            Field(x => x.Value)
                .VisibleIf("info.TransactionType != null")
                .Label(@"@(info.TransactionType == TransactionType.Deposit ? ""Deposit in"" : ""Withdrawal"")");


            Field(x => x.Consignment)
                .Label("Tracking number")
                .VisibleIf("info.TransactionType != TransactionType.Deposit")
                .SourceCriteria("item.Shipment.Company == info.Company")
                .Control(ControlType.AutoComplete);

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.RunInTransaction();
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });

            ViewModelProperty<Company>("Company").FromRequestParam("company");
            OnBound("setting today default value for date added").Code(@"
if (info.Item.IsNew && !info.DateAdded.HasValue)
                info.DateAdded = DateTime.Now;
");
        }
    }
}
