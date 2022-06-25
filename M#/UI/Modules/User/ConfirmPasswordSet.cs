using MSharp;
using Domain;

namespace Modules
{
    public class ConfirmPasswordSet : ViewModule<Domain.User>
    {
        public ConfirmPasswordSet()
        {
            HeaderText("@item.FirstName @item.LastName").Markup("@ContentBlock.PasswordSuccessfullySet.Content");

            Link("Proceed to the login page.").OnClick(x => x.Go<LoginPage>());
        }
    }
}