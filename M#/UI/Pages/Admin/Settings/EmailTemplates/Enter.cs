using MSharp;
namespace Admin.Settings.EmailTemplate
{
    class EnterPage : SubPage<Admin.Settings.EmailTemplatesPage>
    {
        public EnterPage()
        {
            Add<Modules.EmailTemplateForm>();
        }
    }
}