using MSharp;

namespace Modules
{
    class CompanyList : BaseListModule<Domain.Company>
    {
        public CompanyList()
        {

            Search(x => x.Name).Label("Company name")
                .MemoryFilterCode("//remove from here");

            CustomSearch("Company user email")
                .ViewModelName("CompanyUserEmail")
                .ViewModelType("string?").Label("Company user email")
                .MemoryFilterCode("//remove from here");

            Search(x => x.Type).Control(ControlType.HorizontalCheckBoxes).MemoryFilterCode("//remove from here");
            this.ArchiveSearch().MemoryFilterCode("//remove from here");

            SearchButton("Search")
                .IsDefault()
                .Icon(FA.Search)
                .OnClick(x =>
                {
                    x.ReturnView();
                })
                .ExtraTagAttributes("must-url-encode=true");

            LinkColumn(x => x.Name)
                .HeaderText("Company name")
                .OnClick(x => x.Go<Admin.Company.DetailsPage>().Send("company", "item.ID"));

            Column(x => x.CustomerAccountNumber);
            Column(x => x.Address);
            Column(x => x.Country.Code).LabelText("Country");
            Column(x => x.Type);
            //Column(x => x.PaymentCode);
            Column(x => x.PaymentType)
                .DisplayExpression(@"@(item.PaymentType?.Code + "" - "".OnlyWhen(item.PaymentTypeId.HasValue) + item.PaymentType?.Description)");
            Column(x => x.DefermentNumber);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Company.EnterPage>().Send("item", "item.ID").SendReturnUrl()).ColumnVisibleIf(AppRole.SuperAdmin);

            this.ArchiveButtonColumn("Company").ColumnVisibleIf(AppRole.SuperAdmin);

            Button("Bulk Upload").OnClick(x => x.PopUp<Admin.Company.BulkUploadPage>());

            Button("Bulk Upload History").OnClick(x => x.Go<Admin.Company.BulkUploadListPage>());

            Button("New Company").Icon(FA.Plus)
               .OnClick(x => x.Go<Admin.Company.EnterPage>().SendReturnUrl());
        }
    }
}
