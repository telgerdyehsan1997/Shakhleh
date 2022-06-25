using Olive;
using Olive.PDF;
using System.Threading.Tasks;

namespace Domain
{
    public interface IInvoicePdfGenerator
    {
        Task<byte[]> GetPdfBytes(Invoice invoice);
    }
    public class InvoicePdfGenerator : IInvoicePdfGenerator
    {
        const string RELATIVE_URL = "print/invoice";
        readonly IHtml2PdfConverter Converter;

        public InvoicePdfGenerator(IHtml2PdfConverter converter)
        {
            Converter = converter;
        }

        public async Task<byte[]> GetPdfBytes(Invoice invoice)
        {
            var url = RELATIVE_URL + "/" + invoice.ID; // format with params
            url = GetRelativeUrl(url);

            return Converter.GetPdfFromUrlBytes(url);
        }

        private string GetRelativeUrl(string relativePath)
            => Context.Current.Request().GetAbsoluteUrl(relativePath);
    }
}
