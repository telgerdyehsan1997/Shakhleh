using Domain;
using MSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Modules
{
    public class CompanyBulkUploadList : BaseListModule<Domain.ImportQueueItem>
    {
        public CompanyBulkUploadList()
        {
            HeaderText("Company Bulk Uploads")
                .SortingStatement("item.UploadDate DESC");
            SourceCriteria("item.Type == ImportType.Company");

            Search(x => x.UploadDate);
            Search(x => x.Status).AsRadioButtons(Arrange.Horizontal);
            SearchButton("Search").OnClick(x => x.ReturnView());

            Column(x => x.UploadDate);
            CustomColumn("File").LabelText("File").DisplayExpression("@item.File.FileName");
            Column(x => x.Status);

            LinkColumn("Errors").HeaderText("Errors").VisibleIf("await item.Errors.GetList().Any()")
                .OnClick(x => x.Go<Admin.Company.BulkUpload.ErrorsPage>().Send("item", "item.ID"));
        }
    }
}