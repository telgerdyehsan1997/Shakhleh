using MSharp;

namespace Admin.Settings.Routes
{
    class EnterPage : SubPage<RoutesPage>
    {
        public EnterPage()
        {
            Add<Modules.RoutesForm>();
        }

    }
}
