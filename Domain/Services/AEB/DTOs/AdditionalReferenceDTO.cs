namespace Domain.AEB.DTOs
{
    public class AdditionalReferenceDTO : BaseDTO
    {
        public string ReferenceType { get; set; }
        public string Reference { get; set; }
        public DateAndZoneDTO IssueDate { get; set; }
    }
}