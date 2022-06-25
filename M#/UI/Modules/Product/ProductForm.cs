using MSharp;

namespace Modules
{
    class ProductForm : FormModule<Domain.Product>
    {
        public ProductForm()
        {
            HeaderText("Product Details");
            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
            AutoSet(x => x.Company).Value("info.Company");
            RootCssClass("product-form");

            Field(x => x.Name).Label("Description of goods");

            Field(x => x.CommodityCode)
                .ControlWrapperCssClass("product-form")
                .Control(ControlType.AutoComplete)
                .SourceCriteria("!item.IsDeactivated")
                .DisplayExpression("@item.ExportCode + \" - \" + @item.ImportCode")
                //.ChangeEventHandler(@"info.VAT = info.CommodityCode.VAT;")
                .ReloadOnChange()
                .Mandatory();

            Field(x => x.AdditionalCode);
            Field(x => x.Quota);

            CustomField("Second quantity")
                .Label("Second quantity")
                .ItemCssClass("readonly-field")
                .ControlMarkup(@"@info.CommodityCode?.SecondQuantity?.QuantityCode");

            CustomField("Third quantity").Label("Third quantity").ItemCssClass("readonly-field")
                .ControlMarkup(@"@info.CommodityCode?.ThirdQuantity");
            //Field(x => x.UNDangerousGoodsCode).Label("UN Dangerous goods code (if applicable)");

            Field(x => x.VAT)
                .Control(ControlType.DropdownList)
                .DataSource("info.CommodityCode == null ? Enumerable.Empty<VATType>() : await info.CommodityCode.MultipleVAT");

            Field(x => x.Licenced)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons)
                .ReloadOnChange()
                .LabelCssClass("licensed-field");

            Field(x => x.ExportLicence)
                .VisibleIf("info.Licenced == true");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save")
                .IsDefault()
                .Icon(FA.Check)
            .OnClick(x =>
            {
                x.CSharp("info.Item.ExportLicence = null;").If("info.Licenced == false");
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });
        }
    }
}