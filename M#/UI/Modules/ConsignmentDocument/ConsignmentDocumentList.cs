using MSharp;
using Domain;
namespace Modules
{
    class ConsignmentDocumentList : BaseListModule<Domain.ConsignmentDocument>
    {
        public ConsignmentDocumentList()
        {
            HeaderText(@"@(info.Consignment.Shipment.TrackingNumber.ToString()) - @(info.Consignment.ConsignmentNumber.ToString()) - @(info.Consignment.Shipment.IsOutUK ? ""EAD Document"" : ""Import Entry"")");
            DataSource("(await info.Consignment.GetDocuments(GetUser()))");
            this.AddDependency(typeof(IZipDownloadService));

            Column(x => x.Name).LabelText("File Name");

            CustomColumn("Type")
                .LabelText("Type")
                .DisplayExpression("@item.File.FileExtension.ToString()");

            Column(x => x.DateRecieved)
                .LabelText("Date Received")
                .DisplayFormat("{0: dd/MM/yyyy HH:mm:ss}");

            Column(x => x.File)
                .LabelText("Download");

            Button("Download All")
                .OnClick(x => x.CSharp("return await File(await ZipDownloadService.CompressFiles(info.Items.Select(x => x.Item.File),info.Consignment.ConsignmentNumber));"));

            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
        }
    }
}