using MSharp;

namespace Domain
{
    class Deposit : EntityType
    {
        public Deposit()
        {
            var type = Associate<TransactionType>("Transaction type").Mandatory();
            Associate<Company>("Company").Mandatory();
            var consingment = Associate<Consignment>("Consignment");
            DateTime("Date added").Mandatory();
            DateTime("Date created").Mandatory().Default(cs("LocalTime.Now"));
            Decimal("Value").Mandatory();
            Decimal("Remaining balance").CalculatedFrom("Company.GetRemainingBalance(this).GetAwaiter().GetResult()").Min(decimal.MinValue);
            this.Archivable();

            UniqueCombination(new[] { type, consingment });
        }
    }
}
