using MSharp;

namespace Modules
{
    class CompanyCPCForm : FormModule<Domain.CompanySpecialCPCLink>
    {
        public CompanyCPCForm()
        {
            HeaderText("Special CPC Details");

            Field(x => x.CPC).AsAutoComplete().SourceCriteria(@"!item.IsDeactivated");

            AutoSet(x => x.Company).FromRequestParam("company");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });
        }
    }
}