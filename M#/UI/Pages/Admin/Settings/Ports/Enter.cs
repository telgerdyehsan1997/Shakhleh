using MSharp;

namespace Admin.Settings.Ports
{
    class EnterPage : SubPage<Settings.PortsPage>
    {
        public EnterPage()
        {
            Add<Modules.PortForm>();
        }
    }
}