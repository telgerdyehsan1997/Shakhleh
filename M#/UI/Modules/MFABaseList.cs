using MSharp;

namespace Modules
{
    abstract class MFABaseList<T> : ListModule<T> where T : Olive.Entities.IEntity
    {
        protected MFABaseList()
        {
            RootCssClass("mfa");
            this.AddDependency<Domain.IMFAService>();
            this.AddDependency<Domain.ISmsService>();

            OnBound("Validate MFA")
                .Code(@"if (!await HasMFA(MFAService))
                {
                    var message = await MFAService.GenerateMFA(GetUser());
                    AjaxRedirect(Url.Index(""MFAPopUp"", new { ReturnUrl = Url.Current() }), target: ""$modal"");
                }");
        }
    }
}