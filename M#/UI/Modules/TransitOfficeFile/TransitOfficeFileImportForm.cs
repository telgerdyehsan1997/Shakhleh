using MSharp;

namespace Modules
{
    class TransitOfficeFileImportForm : FormModule<Domain.TransitOfficeFile>
    {
        public TransitOfficeFileImportForm()
        {
            Using("System.IO");

            HeaderText("Import [#BUTTONS(DownloadTemplate)#]");

            Field(x => x.File);
            AutoSet(x => x.Status).Value("TransitOfficeFileStatus.Successful");


            Link("Download template")
                .CssClass("float-right download-link-header")
                .CausesValidation(false)
                .OnClick(x => x.CSharp("return File(await System.IO.File.ReadAllBytesAsync(Path.Combine(" +
                "Directory.GetCurrentDirectory(), " +
                "\"Templates\\\\TransitOfficeBulkUpload.csv\")), \"text/csv\", \"TransitOfficeBulkUpload.csv\");"));

            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.CloseModal(Refresh.Full);
            });
        }
    }
}