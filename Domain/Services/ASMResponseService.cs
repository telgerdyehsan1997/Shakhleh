using Olive;
using Olive.Entities;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;
using System.Text;

namespace Domain
{
    public class ASMResponseService : IASMResponseService
    {
        private readonly IDatabase Database = Olive.Context.Current.Database();


        public async Task<ProgressASMResponseVM> GetResponseMessage(Consignment item)
        {
            var result = new ProgressASMResponseVM { Progress = item.Progress.ClientDisplay };

            var latestResponse = await item.Logs
                .OrderByDescending(x => x.Date)
                .FirstOrDefault();

            result.ASMMessage = latestResponse == null ? "No ASM Response Message Found" : (await latestResponse.File.GetContentTextAsync()).Trim();

            return result;
        }

        private async Task<string> GetASMMessage(EadTransactionLog latestResponse)
        {
            var xml = await latestResponse.File.GetContentTextAsync();
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            var xmlMessages = xmldoc.GetElementsByTagName("Message");
            var errorMessage = xmldoc.GetElementsByTagName("ErrorMessage").Item(0);
            var stringbuilder = new StringBuilder();
            stringbuilder.AppendLine(errorMessage?.InnerText);

#pragma warning disable GCop659 // Use 'var' instead of explicit type.
            foreach (XmlElement message in xmlMessages)
                stringbuilder.AppendLine(message.InnerText);
#pragma warning restore GCop659 // Use 'var' instead of explicit type.

            return stringbuilder.ToString();
        }
    }

    public interface IASMResponseService
    {

        Task<ProgressASMResponseVM> GetResponseMessage(Consignment item);

    }
}
