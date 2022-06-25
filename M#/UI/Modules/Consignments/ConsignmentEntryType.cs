using MSharp;

namespace Modules
{
    class ConsignmentEntryType : FormModule<Domain.Consignment>
    {
        public ConsignmentEntryType()
        {
            Header("<p>Would change entry type now and resubmit to ASM/HMRC </p>");
            DataSource("info.Consignment");
            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
            ViewModelProperty<string>("EntryType").FromRequestParam("entryType");

            Field(x => x.UCN).Mandatory().VisibleIf(@"info.EntryType == ""GVMS""");

            Button("No")
                 .OnClick(x =>
                 {
                     x.CloseModal();
                 });

            Button("Yes")
                .OnClick(x =>
                {
                    x.RunInTransaction();
                    x.SaveInDatabase();
                    x.CSharp(@"await IEADShipmentService.CancelDeclaration(info.Item);
                                var portType = new PortType();
                                if (await info.Item.EntryTypeText(info.Item.Shipment.Route.UKPort) == ""GVMS"")
                                    portType = PortType.Inventory;
                                else
                                        portType = PortType.GVMS;

                                    info.Item = await Database.Update(info.Item, x =>
                                    {
                                        x.UCR = info.Item.UCR + ""A"";
                                        x.UCN = info.UCN;
                                        x.EntryReference = null;
                                        x.IntoUKType = portType;

                                    }, SaveBehaviour.BypassAll);
                                    info.Item.NeedToSendAmendment = false;
                                    await IEADShipmentService.Transmit(info.Item, true, true);");

                    x.GentleMessage("Submit successfully.");
                    x.ReturnToPreviousPage();
                });

             OnBeforeSave("Update logic").Code(@"  
                    var documents = await item.Documents.GetList();
                    foreach (var document in documents)
                    {
                        if (document.Name.Contains(""H2"") || document.Name.Contains(""C88""))
                        {
                            await Database.Update(document, x =>
                            {
                                x.Name = info.EntryType + x.Name;
                                x.File.FileName = info.EntryType + x.File.FileName;
                            });
                        }
                    }
            ");
             Inject("IEADShipmentService");

        }
    }
}