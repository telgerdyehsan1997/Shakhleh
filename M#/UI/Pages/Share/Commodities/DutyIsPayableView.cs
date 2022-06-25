using MSharp;

namespace Share.Commodities
{
    class DutyIsPayableViewPage : SubPage<CommoditiesPage>
    {
        public DutyIsPayableViewPage()
        {
            Layout(Layouts.FrontEndModal);
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.DutyIsPayableView>();
        }
    }
}