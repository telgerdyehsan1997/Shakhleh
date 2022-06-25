using MSharp;

namespace Domain
{
    class CPCTaxLine : SubType<TaxLine>
    {
        public CPCTaxLine()
        {
            Associate<CPC>("CPC").Mandatory();
        }
    }
}