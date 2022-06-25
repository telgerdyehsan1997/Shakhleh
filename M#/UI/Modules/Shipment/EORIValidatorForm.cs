using APIHandler;
using MSharp;

namespace Modules
{
    class EORIValidatorForm : FormModule<Domain.Shipment>
    {
        public EORIValidatorForm()
        {
            HeaderText("GB EORI Validator");
            this.AddDependency(typeof(IEORIService));

            CustomField("EORI")
                .Label("EORI")
                .PropertyName("EORI")
                .PropertyType("string")
                .Mandatory()
                .AsTextbox();

            Button("Validate")
                .Text("Validate")
                .OnClick(x => {
                    x.CSharp("var isValid = await EORIService.IsGBEoriNumberValidate(info.EORI);");
                    x.If("isValid")
                        .GentleMessage("EORI is valid.");
                    x.Else()
                        .GentleMessage("EORI is not valid.");
                    x.CloseModal();
                    });

        }
    }
}