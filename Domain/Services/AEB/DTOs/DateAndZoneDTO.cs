namespace Domain.AEB.DTOs
{
    public class DateAndZoneDTO : BaseDTO
    {
        public string DateInTimezone { get; set; }
        public string Timezone { get; set; }
    }
}