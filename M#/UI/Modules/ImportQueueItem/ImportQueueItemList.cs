using Domain;
using MSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Modules
{
    public class ImportQueueItemList : BaseListModule<Domain.ImportQueueItem>
    {
        public ImportQueueItemList()
        {
            HeaderText("Commodity Code Imports")
                .SortingStatement("item.UploadDate DESC");
            SourceCriteria("item.Type == ImportType.CommodityCode");

            Search(x => x.UploadDate);
            Search(x => x.Status).AsRadioButtons(Arrange.Horizontal);
            SearchButton("Search").OnClick(x => x.ReturnView());

            Column(x => x.UploadDate);
            Column(x => x.File);

            Column(x => x.Status);

            LinkColumn("Errors").HeaderText("Errors").VisibleIf("await item.Errors.GetList().Any()")
                .OnClick(x => x.Go<Admin.Settings.Import.ErrorsPage>().Send("item", "item.ID"));

            Button("New Import").IsDefault().Icon(FA.Plus)
                .OnClick(x => x.PopUp<Admin.Settings.Import.UploadPage>());

        }
    }
}