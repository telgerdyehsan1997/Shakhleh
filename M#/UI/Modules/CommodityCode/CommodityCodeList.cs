using MSharp;

namespace Modules
{
    class CommodityCodeList : BaseListModule<Domain.CommodityCode>
    {
        public CommodityCodeList() : base()
        {
            HeaderText("Commodity Codes")
                .SourceCriteria("!item.IsDeactivated");

            RootCssClass("scrollable-list");

            SortingStatement("ExportCode | ImportCode");

            Search(x => x.ExportCode).Label("Find")
            .MemoryFilterCode(@"if (info.ExportCode.HasValue())
            result = result.Where(c => c.ExportCode.StartsWith(info.ExportCode.Trim(), false));");
            SearchButton("Search").Icon(FA.Search).CssClass("float-right").OnClick(x => x.ReturnView());

            Column(x => x.ExportCode);
            Column(x => x.ImportCode);

            //  Column(x => x.DangerousGoods);


            Column(x => x.SecondQuantity);
            Column(x => x.ThirdQuantity);
            Column(x => x.VATString).LabelText("VAT");

            Column(x => x.FullRateOfDuty).LabelText("WTO full rate").DisplayFormat("{0:0.0%}");
            Column(x => x.LIC99);
            Column(x => x.Control);

            Column(x => x.EUQuota);
            Column(x => x.EUQuotaPref);
            Column(x => x.OtherQuota);
            Column(x => x.Box44_1);
            Column(x => x.Box44_2);

            Column(x => x.SpecificRate).LabelText("WTO additional");
            Column(x => x.MFNFullRate);
            Column(x => x.MFNAdditionalDuty);
            Column(x => x.N851_PHC);
            Column(x => x.N852_CED);
            Column(x => x.N853_CVD);
            Column(x => x.ESA_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.ESA_TRQ!=""0""&&!x.Item.ESA_TRQ.IsEmpty())");
            Column(x => x.FAR_PREF).VisibleIf(@"Model.Items.Any(x=>x.Item.FAR_PREF!=""0""&&!x.Item.FAR_PREF.IsEmpty())");
            Column(x => x.FAR_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.FAR_TRQ!=""0""&&!x.Item.FAR_TRQ.IsEmpty())");
            Column(x => x.GSP_PREF).VisibleIf(@"Model.Items.Any(x=>x.Item.GSP_PREF!=""0""&&!x.Item.GSP_PREF.IsEmpty())");
            Column(x => x.GSP_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.GSP_TRQ!=""0""&&!x.Item.GSP_TRQ.IsEmpty())");
            Column(x => x.IND_PREF).VisibleIf(@"Model.Items.Any(x=>x.Item.IND_PREF!=""0""&&!x.Item.IND_PREF.IsEmpty())");
            Column(x => x.IND_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.IND_TRQ!=""0""&&!x.Item.IND_TRQ.IsEmpty())");
            Column(x => x.INO_PREF).VisibleIf(@"Model.Items.Any(x=>x.Item.INO_PREF!=""0""&&!x.Item.INO_PREF.IsEmpty())");
            Column(x => x.INO_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.INO_TRQ!=""0""&&!x.Item.INO_TRQ.IsEmpty())");
            Column(x => x.GS_PREF).VisibleIf(@"Model.Items.Any(x=>x.Item.GS_PREF!=""0""&&!x.Item.GS_PREF.IsEmpty())");
            Column(x => x.GS_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.GS_TRQ!=""0""&&!x.Item.GS_TRQ.IsEmpty())");
            Column(x => x.LDC_PREF).VisibleIf(@"Model.Items.Any(x=>x.Item.LDC_PREF!=""0""&&!x.Item.LDC_PREF.IsEmpty())");
            Column(x => x.LDC_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.LDC_TRQ!=""0""&&!x.Item.LDC_TRQ.IsEmpty())");
            Column(x => x.ISR_PREF).VisibleIf(@"Model.Items.Any(x=>x.Item.ISR_PREF!=""0""&&!x.Item.ISR_PREF.IsEmpty())");
            Column(x => x.ISR_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.ISR_TRQ!=""0""&&!x.Item.ISR_TRQ.IsEmpty())");
            Column(x => x.PAL_PREF).VisibleIf(@"Model.Items.Any(x=>x.Item.PAL_PREF!=""0""&&!x.Item.PAL_PREF.IsEmpty())");
            Column(x => x.PAL_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.PAL_TRQ!=""0""&&!x.Item.PAL_TRQ.IsEmpty())");
            Column(x => x.SWI_PREF).VisibleIf(@"Model.Items.Any(x=>x.Item.SWI_PREF!=""0""&&!x.Item.SWI_PREF.IsEmpty())");
            Column(x => x.SWI_TRQ).VisibleIf(@"Model.Items.Any(x=>x.Item.SWI_TRQ!=""0""&&!x.Item.SWI_TRQ.IsEmpty())");
        }
    }
}