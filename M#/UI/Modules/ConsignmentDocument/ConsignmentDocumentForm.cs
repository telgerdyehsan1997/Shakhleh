using Domain;
using MSharp;

namespace Modules
{
    class ConsignmentDocumentForm : FormModule<Domain.ConsignmentDocument>
    {
        public ConsignmentDocumentForm()
        {
            this.AddDependency(typeof(IFileProcessorService));

            Field(x => x.File).Label("Upload Zip/Pdf file").AsFileUpload();

            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");

        }
    }
}