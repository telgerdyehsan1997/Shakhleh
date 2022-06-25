using MSharp;

namespace Modules
{
    class HealthCertificateForm : FormModule<Domain.HealthCertificate>
    {
        public HealthCertificateForm()
        {
            HeaderText("Health Certificate");

            Field(x => x.Code).Mandatory();
            Field(x => x.Description).Mandatory();

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