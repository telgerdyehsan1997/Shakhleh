using MSharp;

namespace Modules
{
    class UpdateConsignmentProgressForm : FormModule<Domain.Consignment>
    {
        public UpdateConsignmentProgressForm()
        {
            Field(x => x.Progress)
                .DataSource("await info.Consignment.Shipment.GetProgress()");
            
            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.CSharp("await info.Consignment.ManualUpdate(info.Progress , Context.Current.User().ExtractUser<User>());");
                x.GentleMessage("Saved successfully.");
                x.CloseModal(Refresh.Full);
            });

            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");

        }
    }
}