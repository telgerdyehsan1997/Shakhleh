using MSharp;
using Domain;

namespace Customer.Settings
{
    public class RecordPage : SubPage<SettingsPage>
    {
        public RecordPage()
        {
            Add<Modules.RecordDetailForm>();
        }
    }
}