using System.Collections.Generic;

namespace Domain.AEB.DTOs
{
    public class DeliveryItemEUDTO : BaseDTO
    {
        public string TransactionNatureCode { get; set; }
        public List<ProducedDocumentEUDTO> ProducedDocuments { get; set; }
    }
}