using MSharp;
using Domain;

namespace Modules
{
    public class ChannelPortsUserForm : FormModule<Domain.ChannelPortsUser>
    {
        public ChannelPortsUserForm()
        {
            HeaderText("User Details");

            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Email);
            Field(x => x.MobileNumber).Label("Phone Number");
            Field(x => x.IsAdmin)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons)
                .Label("Admin");


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