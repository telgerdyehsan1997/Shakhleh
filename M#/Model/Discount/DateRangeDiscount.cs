using MSharp;

namespace Domain
{
    class DateRangeDiscount : SubType<Discount>
    {
        public DateRangeDiscount()
        {
            Date("Start").Mandatory();
            Date("End");
        }
    }
}
