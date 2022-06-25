using MSharp;

namespace Modules
{
    class CommodityProductForm : FormModule<Domain.Product>
    {
        public CommodityProductForm()
        {
            HeaderText("Product Details");
            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
            SupportsAdd(true);
            SupportsEdit(false);

            Field(x => x.Name)
                .Label("Description of Goods");

            Field(x => x.CommodityCode)
                .Control(ControlType.AutoComplete)
                .Mandatory()
                .SourceCriteria("!item.IsDeactivated")
                .DisplayExpression("@item.ExportCode + (\" - \" + @item.ImportCode).OnlyWhen(@item.ImportCode.HasValue())")
                .RequiredValidationMessage("A valid Commodity Code is required.")
                .ReloadOnChange();


            Field(x => x.VAT)
                 .Control(ControlType.DropdownList)
                 .VisibleIf("info.CommodityCode != null && (await info.CommodityCode.MultipleVAT).HasMany()")
                 .DataSource("info.CommodityCode == null ? Enumerable.Empty<VATType>() : await info.CommodityCode.MultipleVAT");

            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.RunInTransaction();
                x.MessageBox("A valid Commodity Code is required.").Style("error").If("info.CommodityCode == null").AndExit();
                x.CSharp("await CreateNewProducts(info);").ValidationError();
                x.GentleMessage("Saved successfully.");
                x.CloseModal(Refresh.Full);
            });
        }
    }
}