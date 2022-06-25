using Olive;
using Olive.PDF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Winnovative;

namespace Domain
{
    public class Html2PdfConverter : HtmlToPdfConverter, IHtml2PdfConverter
    {
        public Html2PdfConverter()
        {
            LicenseKey = Config.Get("Olive.Html2Pdf:LicenseKey");
        }

        public byte[] GetPdfFromUrlBytes(string url) => ConvertUrl(url);
    }
}
