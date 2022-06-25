using System;
using System.Collections.Generic;
using System.Text;
using MSharp;

namespace Modules
{
    class ShipmentAttachmentUploadForm : FormModule<Domain.UploadAttachment>
    {
        public ShipmentAttachmentUploadForm()
        {
            HeaderText("Upload");

            Field(x => x.Attachment);
            AutoSet(x => x.Shipment).Value("info.Shipment");
            
            Button("Cancel")
                .Text("Cancel")
                .OnClick(x => x.CloseModal());

            Button("Upload")
                .Text("Upload")
                .OnClick(x => {
                    x.SaveInDatabase();
                    x.CloseModal(Refresh.Full);
                });

            ViewModelProperty<Domain.Shipment>("Shipment").FromRequestParam("shipment");
        }
    }
}
