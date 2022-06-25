namespace Domain.AEB.DTOs
{
    public class TextInLanguageDTO : BaseDTO
    {
        public string LanguageISOCode { get; set; }
        public string Text { get; set; }
    }
}