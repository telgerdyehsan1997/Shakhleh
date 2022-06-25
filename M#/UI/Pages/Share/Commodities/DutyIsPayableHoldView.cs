using MSharp;

namespace Share.Commodities
{
    class DutyIsPayableHoldViewPage : SubPage<CommoditiesPage>
    {
        public DutyIsPayableHoldViewPage()
        {
            Layout(Layouts.FrontEndModal);
            Roles(AppRole.Admin, AppRole.Customer);
            Add<Modules.DutyIsPayableHoldView>();
        }
    }
}