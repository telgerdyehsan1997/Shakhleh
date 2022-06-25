using MSharp;
using Domain;

namespace Customer.Settings
{
    public class GeneralPage : SubPage<SettingsPage>
    {
        public GeneralPage()
        {
            Add<Modules.RecordDetailForm>();
        }
    }
}