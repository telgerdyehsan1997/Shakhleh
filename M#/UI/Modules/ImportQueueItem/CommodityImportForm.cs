using MSharp;

namespace Modules
{
    class CommodityImportForm : FormModule<Domain.ImportQueueItem>
    {
        public CommodityImportForm()
        {
            Using("System.IO");
            this.AddDependency<Domain.ICommodityImportService>();
            HeaderText("Import [#BUTTONS(DownloadTemplate)#]")
                .SupportsAdd(true)
                .SupportsEdit(false);

            Field(x => x.File);
            AutoSet(x => x.Type).Value("ImportType.Commodity");
            AutoSet(x => x.Consignment).FromRequestParam("consignment");
            AutoSet(x => x.Company)
                .Value("info.Consignment.Shipment.Company");
            
            
            Button("Cancel").OnClick(x => x.CloseModal());
            
            Button("Import").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.RunInTransaction(false);
                x.SaveInDatabase();
                x.CSharp("var succeeded = await CommodityImportService.ProcessImport(info.Item);");
                x.If("succeeded")
                    .GentleMessage("Saved successfully.");
                x.Else()
                    .PopUp<Share.Commodities.Import.ErrorLogPage>().Send("item", "info.Item.ID.ToString()").SendReturnUrl();
                x.If("succeeded")
                    .CloseModal();
            });

            Link("Download template")
                 .CssClass("float-right download-link-header")
                 .CausesValidation(false)
                 .OnClick(x => x.CSharp("return File(await System.IO.File.ReadAllBytesAsync(Path.Combine(" +
                "Directory.GetCurrentDirectory(), " +
                "\"Templates\\\\CommodityImport.csv\")), \"text/csv\", \"CommodityImport.csv\");"));
        }
    }
}