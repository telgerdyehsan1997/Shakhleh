using MSharp;

namespace Modules
{
    class GuaranteeLengthForm : FormModule<Domain.GuaranteeLength>
    {
        public GuaranteeLengthForm()
        {
            HeaderText("Guarantee Length");
            
            Field(x => x.Length).Label("Valid for (days)");

            AutoSet(x => x.AuthorisedLocation).FromRequestParam("location");

            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.CloseModal(Refresh.Full);
            });
        }
    }
}