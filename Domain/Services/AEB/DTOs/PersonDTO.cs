namespace Domain.AEB.DTOs
{
    public class PersonDTO : BaseDTO
    {
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Supplement { get; set; }
        public string Initials { get; set; }
        public bool IsDeactivated { get; set; }
    }
}