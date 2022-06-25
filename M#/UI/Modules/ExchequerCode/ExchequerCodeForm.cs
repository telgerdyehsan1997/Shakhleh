using MSharp;

namespace Modules
{
    class ExchequerCodeForm : MFABaseForm<Domain.ExchequerCode>
    {
        public ExchequerCodeForm()
        {
            HeaderText("Exchequer Code Details");
            
            Field(x => x.NominalCode);
            Field(x => x.CostCentre);
            Field(x => x.Department);
            
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