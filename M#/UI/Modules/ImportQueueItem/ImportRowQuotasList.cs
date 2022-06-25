using MSharp;

namespace Modules
{
    class ImportRowQuotasList : ListModule<Domain.ImportQueueItem>
    {
        public ImportRowQuotasList()
        {
            HeaderText("ROW Quota Imports")
                 .SortingStatement("item.UploadDate DESC");
            SourceCriteria("item.Type == ImportType.RowQuota");

            Search(x => x.UploadDate).Label("Date");
            Search(x => x.Status).AsRadioButtons(Arrange.Horizontal).Label("Import status");
            SearchButton("Search").OnClick(x => x.ReturnView());

            Column(x => x.UploadDate);
            Column(x => x.File);

            Column(x => x.Status);

            LinkColumn("Errors").HeaderText("Errors").VisibleIf("await item.Errors.GetList().Any()")
                .OnClick(x => x.Go<Admin.Settings.Import.RowQuotaErrorLogPage>().Send("item", "item.ID").SendReturnUrl());

            Button("New Import").IsDefault().Icon(FA.Plus)
                .OnClick(x => x.PopUp<Admin.Settings.Import.UploadRowQuotaPage>());

        }
    }
}