using MSharp;

namespace Modules
{
    class PortForm : FormModule<Domain.Port>
    {
        public PortForm()
        {
            HeaderText("Port Details");

            Field(x => x.PortName);
            Field(x => x.TransportMode);
            Field(x => x.Country).Mandatory();

            Field(x => x.Non_UK)
              .Mandatory()
              .Control(ControlType.HorizontalRadioButtons)
              .ReloadOnChange()
              .CustomDataSave(@"if(info.Non_UK == true){
                                  item.UKBFEmail = """";
                                  item.TitledLocationCode = """";
                                  }");

            Field(x => x.PortCode);
            Field(x => x.TitledUsePortCode).Label("Use Port Code");
            Field(x => x.TitledLocationCode).Label("Location Code").VisibleIf("info.Non_UK == false");

            Field(x => x.TransitOffice).Label("NCTS code").Control(ControlType.AutoComplete).RequiredValidationMessage("The NCTS code field is required.").DisplayExpression("item.ToString() +  \" - \" + item.NCTSCode");

            Field(x => x.UKBFEmail).VisibleIf("info.Non_UK == false");

            Field(x => x.PortsIntoUk).Label("Into UK Type").DataSource("").Control(ControlType.HorizontalCheckBoxes).Mandatory();
            Field(x => x.IntoUKValue);
            Field(x => x.OutOfUKType).Control(ControlType.HorizontalRadioButtons).Mandatory();
            Field(x => x.OutOfUKValue);
            Field(x => x.DTIBadge);


            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });
        }
    }
}