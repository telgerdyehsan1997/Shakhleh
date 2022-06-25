namespace Domain.AEB.DTOs
{
    public class TransportMeansDTO : BaseDTO
    {
        public string MeansType { get; set; }
        public string TransportModeCode { get; set; }
        //public string TransportMeansCode { get; set; }
        public string Identification { get; set; }
        //public string NationalityCode { get; set; }
    }
}