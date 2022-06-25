using MSharp;

namespace Modules
{
    class MFAForm : FormModule<Domain.MFAViewModel>
    {
        public MFAForm()
        {
            RootCssClass("mfa");
            this.AddDependency<Domain.IMFAService>();

            HeaderText("Two Factor Authentication");
            CustomField("Message")
                .ControlMarkup("you will recieve a code via SMS, please enter the code below to access the page.");
            Field(x => x.AccessCode)
                .Label("Access Code");

            Button("Cancel")
                .OnClick(x =>
                {
                    x.CloseModal();
                    x.Go<AdminPage>();
                });

            Button("Submit")
               
                .OnClick(x =>
                {
                    x.CSharp(@"if(await MFAService.ValidateMFA(GetUser(),info.AccessCode) != MFAStatus.Allowed)  
                                        throw new Olive.Entities.ValidationException(""The code is not valid."");").ValidationError();
                    x.CSharp(@"SaveInSession(info.AccessCode,Constants.MFAKey);");
                    x.GentleMessage("Your session is valid for 20 minutes");
                    x.CloseModal();
                });
        }
    }
}