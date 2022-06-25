using MSharp;

namespace Domain
{
    class Ancillary : EntityType
    {
        public Ancillary()
        {
            this.Archivable();
            var country = Associate<Country>("Country").Mandatory();
            var company = Associate<Company>("Company").DatabaseIndex(true);
            Money("Freight charge per tonne");
            Money("Full load freight charge");
            Money("Value for VAT");
            Percent("Insurance charge").Scale(5);

            UniqueCombination(new[] { country, company });
        }
    }
}
