using MSharp;

namespace Modules
{
    class EmailTemplateList : BaseListModule<Domain.EmailTemplate>
    {
        public EmailTemplateList()
        {
            HeaderText("Email Templates");

            Column(x => x.Name).LabelText("Key");
            Column(x => x.Subject);
            Column(x => x.Body);
            Column(x => x.MandatoryPlaceholders);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.EmailTemplate.EnterPage>().Send("item", "item.ID").SendReturnUrl());

        }
    }
}