using MSharp;

namespace Modules
{
    class EmailTemplateForm : FormModule<Domain.EmailTemplate>
    {
        public EmailTemplateForm()
        {
            HeaderText("Email Template Details");

            Field(x => x.Name).Readonly().Label("Key");
            Field(x => x.Subject);
            Field(x => x.Body).Control(ControlType.HtmlEditor);
            Field(x => x.MandatoryPlaceholders).Readonly();

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