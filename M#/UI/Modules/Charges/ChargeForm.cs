using MSharp;

namespace Modules
{
    class ChargeForm : MFABaseForm<Domain.Charge>
    {
        public ChargeForm()
        {
            HeaderText("Custom License Fee Details");
            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
            AutoSet(x => x.Company).Value("info.Company");
            AutoSet(x => x.Currency).Value("info.DefaultLicense?.Currency");
            AutoSet(x => x.IsDefault).Value("false");

            Field(X => X.Name);
            Field(x => x.ValidFrom);
            CustomField("Default License")
                .PropertyName("DefaultLicense")
                .PropertyType("Charge")
                .Label("Default License")
                .Control(ControlType.DropdownList)
                .DataSource("await Database.Of<Charge>().Where(x => x.Name != \"Custom\" && x.IsDefault).GetList()")
                .ItemDisplayExpression("@item.Name + \" - \" + @item.Currency")
                .ChangeEventHandler(@"info.Item.Currency = info.DefaultLicense.Currency;
            info.LicenseFee = info.DefaultLicense.LicenseFee;
            info.FreeConsignments = info.DefaultLicense.FreeConsignments;
            info.PricePerAdditionalConsignment = info.DefaultLicense.PricePerAdditionalConsignment;
            info.PricePerCommodity = info.DefaultLicense.PricePerCommodity;")
                                        
                .ReloadOnChange()
                .Mandatory();
            Field(x => x.Currency).Readonly();
            Field(x => x.IsYearly)
                .Label("Is Charged Yearly");
            Field(x => x.LicenseFee);
            Field(x => x.FreeConsignments);
            Field(x => x.PricePerAdditionalConsignment);
            Field(x => x.PricePerCommodity);
           
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