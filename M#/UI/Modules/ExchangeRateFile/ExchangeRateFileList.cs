using Microsoft.AspNetCore.Mvc.RazorPages;
using MSharp;

namespace Modules
{
    class ExchangeRateFileList : ListModule<Domain.ExchangeRateFile>
    {
        public ExchangeRateFileList()
        {
            HeaderText("Exchange Rates");
            SortingStatement("item.SubmitDate DESC");

            Column(x => x.Name);
            Column(x => x.URL);
            Column(x => x.SubmitDate).HeaderTemplate("Upload Date");
            LinkColumn("Rates").OnClick(x => x.Go<Admin.Settings.ExchangeRatePage>().Send("item", "item.ID"));
        }
    }
}