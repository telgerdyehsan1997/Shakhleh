using MSharp;

namespace Domain
{
    class CommodityCode : EntityType
    {
        public CommodityCode()
        {
            this.Archivable();
            String("Commodity code (export)").Name("ExportCode").Mandatory();
            String("Commodity code (import)").Name("ImportCode").Mandatory();
            Associate<SecondQuantityDescription>("Second quantity");
            Associate<SecondQuantityDescription>("Third quantity");
            // Associate<VATType>("VAT").Mandatory();
            AssociateManyToMany<VATType>("Multiple VAT").Mandatory();
            String("VAT string");
            Percent("Full rate of duty").Mandatory();
            String("Specific rate");
            String("MFN full rate");
            String("MFN additional duty");
            //String("CHI - PREF").Name("CHI_PREF");
            //String("CHI - TRQ").Name("CHI_TRQ");
            //String("ESA - PREF").Name("ESA_PREF");
            //String("ESA - TRQ").Name("ESA_TRQ");
            Bool("N851 - PHC").Name("N851_PHC");
            Bool("N852 - CED").Name("N852_CED");
            Bool("N853 - CVD").Name("N853_CVD");
            String("ESA - TRQ").Name("ESA_TRQ");
            String("FAR - PREF").Name("FAR_PREF");
            String("FAR - TRQ").Name("FAR_TRQ");
            String("GSP - PREF").Name("GSP_PREF");
            String("GSP - TRQ").Name("GSP_TRQ");
            String("IND - PREF").Name("IND_PREF");
            String("IND - TRQ").Name("IND_TRQ");
            String("INO - PREF").Name("INO_PREF");
            String("INO - TRQ").Name("INO_TRQ");
            String("GS+ - PREF").Name("GS_PREF");
            String("GS+ - TRQ").Name("GS_TRQ");
            String("LDC - PREF").Name("LDC_PREF");
            String("LDC - TRQ").Name("LDC_TRQ");
            String("ISR - PREF").Name("ISR_PREF");
            String("ISR - TRQ").Name("ISR_TRQ");
            String("PAL - PREF").Name("PAL_PREF");
            String("PAL - TRQ").Name("PAL_TRQ");
            String("SWI - PREF").Name("SWI_PREF");
            String("SWI - TRQ").Name("SWI_TRQ");
            Bool("LIC99").Mandatory().Default(cs("false"));
            Bool("Control").Mandatory().Default(cs("false"));
            Bool("Dangerous Goods").Mandatory().Default(cs("false"));

            String("EU Quota");
            Bool("Other Quota");
            String("Box 44 (1)");
            String("Box 44 (2)");
            // UniqueCombination(new[] { "Commodity code (export)", "Commodity code (import)" });
            String("EU Quota Pref");

            ToStringExpression("ExportCode + \" - \" + ImportCode");

        }
    }
}