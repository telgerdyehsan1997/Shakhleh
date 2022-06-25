using Domain;
using MSharp;

namespace Modules
{
    class CompanyDepositList : MFABaseList<Domain.Deposit>
    {
        public CompanyDepositList()
        {
            Header(@"@await Component.InvokeAsync(""CompanyDepositView"")");

            PageSize(10);
            SourceCriteria("item.Company == info.Company && item.TransactionTypeId.IsAnyOf(TransactionType.Deposit, TransactionType.Withdrawal) && item.Value > 0");
            Sortable();
            SortingStatement("item.DateAdded DESC | item.DateCreated DESC");
            Button("New Deposit")
                .OnClick(x =>
                x.If(AppRole.Admin).Go<Admin.Company.Deposits.EnterPage>().Pass("company").SendReturnUrl());

            Search(x => x.Consignment)
                .Label("Tracking Number")
                .AsAutoComplete();

            Search(x => x.DateAdded);
            this.ArchiveSearch();

            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

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
            Column(x => x.Consignment.Shipment.MyReferenceForCPInvoice)
                .LabelText("Customer Reference");
            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
            .OnClick(x => x.Go<Admin.Company.Deposits.EnterPage>().Send("deposit", "item.ID").Pass("company").SendReturnUrl()).ColumnVisibleIf(AppRole.SuperAdmin);
            this.ArchiveButtonColumn().ColumnVisibleIf(AppRole.SuperAdmin);


            ViewModelProperty<Company>("Company").FromRequestParam("company");
        }
    }
}
