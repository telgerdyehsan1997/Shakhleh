using MSharp;

namespace Admin.Settings.PaymentType
{
    class EnterPage : SubPage<Settings.PaymentTypePage>
    {
        public EnterPage()
        {
            Add<Modules.PaymentTypeForm>();
        }
    }
}