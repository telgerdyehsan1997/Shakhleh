using MSharp;

namespace Modules
{
    class ASMFileErrorLogList : BaseListModule<Domain.ReportErrorLog>
    {
        public ASMFileErrorLogList()
        {
            HeaderText("Error Logs");

            Search(GeneralSearch.AllFields)
                .Label("Find");
            Search(x => x.RecievedDate)
                .Label("Date");

            SearchButton("Search").Text("Search")
                .OnClick(x => x.ReturnView());


            Button("Resolve all").Text("Resolve All").OnClick(x =>
            {
                x.CSharp(@"if (info.SelectedIds.None())
                return Notify(""Please select the items you wish to resolve."", ""error"");
                else
                {
                    await Database.Delete(await info.SelectedItems);
                }");
                x.RefreshPage();
            });

            SelectCheckbox(true);
            Column(x => x.RecievedDate).DisplayFormat("{0: dd/MM/yyyy HH:mm:ss}");
            LinkColumn(x => x.FileName).OnClick(x => x.CSharp("return File(GetDownloadFile(item), \"aplication/zip\", item.FileName);"));
            Column(x => x.Error);
            Column(x => x.Location);

            ButtonColumn("Action").HeaderText("Attach").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.ErrorLog.AttachLogPage>().Send("item", "item.ID").SendReturnUrl());

            ButtonColumn("Resolve").HeaderText("Resolve").GridColumnCssClass("actions")
                .OnClick(x =>
                {
                    x.DeleteItem();
                    x.RefreshPage();
                });
        }
    }
}