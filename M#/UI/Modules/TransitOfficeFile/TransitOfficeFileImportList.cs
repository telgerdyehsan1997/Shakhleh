using MSharp;

namespace Modules
{
    class TransitOfficeFileImportList : BaseListModule<Domain.TransitOfficeFile>
    {
        public TransitOfficeFileImportList()
        {

            HeaderText("Import History")
             .SortingStatement("item.Date DESC");


            Search(x => x.Date);
            Search(x => x.Status).AsRadioButtons(Arrange.Horizontal);
            SearchButton("Search").OnClick(x => x.ReturnView());


            Column(x => x.Date);
            Column(x => x.Status);
            Column(x => x.File);

            LinkColumn("Errors").HeaderText("Error log")
                .VisibleIf("await item.Errors.GetList().Any()")
                .OnClick(x => x.Go<Admin.Settings.TransitOffices.BulkImport.ErrorsPage>().Send("transitOfficeFile", "item.ID"));


            Button("Back").OnClick(x => x.ReturnToPreviousPage());

            Button("Import").OnClick(x => x.PopUp<Admin.Settings.TransitOffices.BulkImport.ImportPage>().SendReturnUrl());
        }
    }
}