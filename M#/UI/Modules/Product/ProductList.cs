using MSharp;
using Domain;

namespace Modules
{
    class ProductList : BaseListModule<Domain.Product>
    {
        public ProductList() : base()
        {
            HeaderText("Products");
            DataSource("await info.Company.Products.GetList().Where(p=> p.IsCreatedFromAPI == false)");
            EmptyMarkup("There are no Products to display");

            Button("Bulk Upload").OnClick(x => x.PopUp<Admin.Company.Product.BulkUploadPage>().Send("company", "info.Company.ID"));

            Button("Bulk Upload History").OnClick(x => x.Go<Admin.Company.Product.BulkUploadListPage>().Send("company", "info.Company.ID"));

            Button("New Product").IsDefault().Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Company.Product.EnterPage>().Send("company", "info.Company.ID").SendReturnUrl());

            Search(x => x.Name)
                .Label("Description of Goods");

            Search(x => x.Code)
                .Label("Product Code");

            CustomSearch("Commodity Code")
              .ViewModelName("CommodityCode")
              .ViewModelType("string")
              .Label("Commodity Code")
              .MemoryFilterCode(@"if (info.CommodityCode.HasValue() && info.CommodityCode.Length >= 2)
                                 {
                                    result = result.Where(p => p.CommodityCode.ExportCode.StartsWith(info.CommodityCode));
                                }");
       

            Search(x => x.IsDeactivated).Control(ControlType.HorizontalRadioButtons)
                .Label("Status")
                .DefaultValueExpression("false");

            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Name)
                .DisplayExpression("@(item.Name  + \" - \" + item.Code)")
                .LabelText("Description of Goods");

            Column(x => x.CommodityCode).DisplayExpression("@item.CommodityCode.ExportCode - @item.CommodityCode.ImportCode");
            Column(x => x.AdditionalCode).LabelText("Addl code");
            Column(x => x.Quota);
            Column(x => x.CommodityCode.SecondQuantity);
            Column(x => x.CommodityCode.ThirdQuantity);
            Column(x => x.VAT);
            Column(x => x.Licenced);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Company.Product.EnterPage>().Send("item", "item.ID").Send("company", "info.Company.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Product");

            ViewModelProperty<Company>("Company").FromRequestParam("company");


        }
    }
}