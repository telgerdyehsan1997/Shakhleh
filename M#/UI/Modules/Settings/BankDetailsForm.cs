using MSharp;

namespace Modules
{
    class BankDetailsForm : MFABaseForm<Domain.Settings>
    {
        public BankDetailsForm()
        {
            HeaderText("Bank Details");
            DataSource("Domain.Settings.Current");

            Field(x => x.Bankers);
            Field(x => x.SortCode)
                .Label("Sort Code");
            Field(x => x.AccountNo)
                .Label("Account No");
            Field(x => x.IBAN);
            Field(x => x.BIC);

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());
            
            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.RefreshPage();
            });
        }
    }
}