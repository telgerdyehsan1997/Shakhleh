using Domain;
using MSharp;

namespace Modules
{
    class AttachASMFileConsignmentList : ListModule<Domain.Consignment>
    {
        public AttachASMFileConsignmentList()
        {
            VisibleIf("info.Type == ShipmentBaseType.EAD");
            HeaderText("Consignment");
            DataSource("await FileErrorAttachViewModelService.GetConsignments(info.UCR)");

            this.AddDependency<IFileErrorAttachViewModelService>();
            this.AddDependency<IFileProcessorService>();

            Column(x => x.ConsignmentNumber).LabelText("consignment number");

            Column(x => x.UCR)
                .LabelText("Declaration Unique Consignment Reference (DUCR)");

            Column(x => x.Progress);

            LinkColumn("Logs").HeaderText("Logs").Text(@"@await item.Logs.Count()")
              .OnClick(x =>
              {
                  x.Go<Share.Consignments.ConsignmentLogsPage>().Send("consignment", "item.ID").SendReturnUrl();
              });
            
            //ead Docu

            ViewModelProperty<Domain.ReportErrorLog>("ReportErrorLog").FromRequestParam("item");
            ViewModelProperty<Domain.ShipmentBaseType>("Type").FromRequestParam("type");
            ViewModelProperty<string>("UCR").FromRequestParam("ucr");

        }
    }
}