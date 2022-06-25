using MSharp;

namespace Modules
{
    class BannerMessageForm : FormModule<Domain.BannerMessage>
    {
        public BannerMessageForm()
        {
            HeaderText("Banner Message Details");

            Field(x => x.Message);
          
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