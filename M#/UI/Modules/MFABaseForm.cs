using MSharp;

namespace Modules
{
    abstract class MFABaseForm<T> : FormModule<T> where T : Olive.Entities.IEntity
    {
        protected MFABaseForm()
        {
            RootCssClass("mfa");
            this.AddDependency<Domain.IMFAService>();
            this.AddDependency<Domain.ISmsService>();

            OnBound("Validate MFA")
                .Code(@"if (!await HasMFA(MFAService) && Request.IsGet())
                {
                    var message = await MFAService.GenerateMFA(GetUser());
                    AjaxRedirect(Url.Index(""MFAPopUp"", new { ReturnUrl = Url.Current() }), target: ""$modal"");
                }");
        }
    }
}