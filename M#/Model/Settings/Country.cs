using MSharp;

namespace Domain
{
    class Country : EntityType
    {
        public Country()
        {
            this.Archivable();

            String("Name").Mandatory();
            String("Code").Mandatory().Max(2);
            Bool("Preference available").Mandatory(value: false);
            Bool("Has MFN")
                .Mandatory(value: false);
            String("MFN code 1").MinLength(3).Max(3);
            Associate<CPC>("Import CPC with preference");
            String("Import CPC with preference declaration type").MinLength(2).Max(2);
            String("Import CPC with preference preference code").MinLength(3).Max(3);
            String("Import CPC with preference Rate Code").Max(2);
            Associate<CPC>("Import CPC without preference").Mandatory();
            String("Import CPC without preference declaration type").Mandatory().MinLength(2).Max(2);
            String("Import CPC without preference preference code").Mandatory().MinLength(3).Max(3);
            String("Import CPC without preference Rate Code").Max(2);
            Associate<CPC>("Export CPC with preference");
            String("Export CPC with preference declaration type").MinLength(2).Max(2);
            Associate<CPC>("Export CPC without preference").Mandatory();
            String("Export CPC without preference declaration type").Mandatory().MinLength(2).Max(2);
            String("Invoice declaration document type").MinLength(4).Max(4).Mandatory();
            String("Invoice declaration document type document status").MinLength(2).Max(2).Mandatory();
            String("Preference certificate number document type").MinLength(4).Max(4).Mandatory();
            String("Preference certificate number document type document status").MinLength(2).Max(2).Mandatory();
            Bool("EU27").Mandatory(value: false);
            String("Country dialling code").Mandatory();
            String("Country and dial code").Calculated().CalculatedFrom(@"string.Format(""{0} ({1})"", Code, CountryDiallingCode)");
            String("MFN code 2").MinLength(3).Max(3);
            String("MFN code 3").MinLength(3).Max(3);
            String("MFN code 4").MinLength(3).Max(3);
            String("MFN code 5").MinLength(3).Max(3);



            UniqueCombination(new[] { "Code", "Name" });
        }
    }
}