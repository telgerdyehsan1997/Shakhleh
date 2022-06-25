using MSharp;

namespace Modules
{
    class ImportAuthorisedLocationForm : FormModule<Domain.ImportQueueItem>
    {
        public ImportAuthorisedLocationForm()
        {
            Using("System.IO");
            HeaderText("Upload Authorised Locations [#BUTTONS(DownloadTemplate)#]")
                .SupportsAdd(true)
                .SupportsEdit(false);

            Field(x => x.File);

            AutoSet(x => x.Type).Value("ImportType.AuthorisedLocation");


            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.CloseModal(Refresh.Full);
            });

            Link("Download template")
                 .CssClass("float-right download-link-header")
                 .CausesValidation(false)
                 .OnClick(x => x.CSharp("return File(await System.IO.File.ReadAllBytesAsync(Path.Combine(" +
                "Directory.GetCurrentDirectory(), " +
                "\"Templates\\\\AuthorisedLocationsImport.csv\")), \"text/csv\", \"AuthorisedLocationsImport.csv\");"));

        }
    }
}