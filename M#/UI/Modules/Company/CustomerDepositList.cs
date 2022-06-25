using Domain;
using MSharp;

namespace Modules
{
    class CustomerDepositList : ListModule<Domain.Deposit>
    {
        public CustomerDepositList()
        {
            PageSize(10);
            SourceCriteria("item.Company == info.Company");
            Sortable();
            SortingStatement("item.DateAdded DESC | item.DateCreated DESC");

            Search(x => x.Consignment)
                .Label("Tracking Number")
                .AsAutoComplete();

            Search(x => x.DateAdded);
            this.ArchiveSearch();

            SearchButton("Search").IsDefault().Icon(FA.Search)
               .OnClick(x => x.ReturnView());

            Column(x => x.DateAdded)
                .DisplayFormat("{0:d}");
            Column(x => x.Value)
                .LabelText("Deposit In")
                .DisplayExpression(@"@(item.Value.ToString(""c""))")
                .CellVisibleIf("item.TransactionType == TransactionType.Deposit");
            CustomColumn("WithdrawalValue")
                .LabelText("Withdrawal")
                .DisplayExpression(@"@(item.Value.ToString(""c""))")
                .CellVisibleIf("item.TransactionType == TransactionType.Withdrawal");
            Column(x => x.RemainingBalance)
                .LabelText("Remaining Balance")
                 .DisplayExpression(@"@(item.RemainingBalance.ToString(""c""))");
            Column(x => x.Consignment)
                .LabelText("Tracking Number");
            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
            .OnClick(x => x.Go<Admin.Company.Deposits.EnterPage>().Send("deposit", "item.ID").Pass("company").SendReturnUrl()).ColumnVisibleIf(AppRole.SuperAdmin);
            this.ArchiveButtonColumn().ColumnVisibleIf(AppRole.SuperAdmin);


            ViewModelProperty<Company>("Company").FromRequestParam("company");
        }
    }
}
