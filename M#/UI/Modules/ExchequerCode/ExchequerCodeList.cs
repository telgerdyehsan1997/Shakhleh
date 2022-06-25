using MSharp;

namespace Modules
{
    class ExchequerCodeList : MFABaseList<Domain.ExchequerCode>
    {
        public ExchequerCodeList()
        {
            HeaderText("Exchequer Codes");
            
            Column(x => x.NominalCode);
            Column(x => x.CostCentre);
            Column(x => x.Department);

            ButtonColumn("Edit").HeaderText("Actions").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.PopUp<Admin.Accounting.ExchequerCodes.EnterPage>()
                        .Send("item", "item.ID")
                        .SendReturnUrl());

        }
    }
}