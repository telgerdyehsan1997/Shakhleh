using MSharp;

namespace Modules
{
    class NoteForm : FormModule<Domain.Note>
    {
        public NoteForm()
        {
            HeaderText("Note Details");

            Field(x => x.Description).Label("Note");

            AutoSet(x => x.DateAndtime).Value("LocalTime.Now");
            AutoSet(x => x.AddedBy).Value("GetUser()");
            AutoSet(x => x.Company).Value("info.Company");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}