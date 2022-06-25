using Olive;
using Olive.Entities;
using System.Threading.Tasks;
using System.Linq;

namespace Domain
{
    public interface IDocumentService
    {
        Task SendEADDeliveryDocuments();

    }

    public class DocumentService : IDocumentService
    {
        readonly IDatabase Database;

        public DocumentService(IDatabase database)
        {
            Database = database;
        }

        public async Task SendEADDeliveryDocuments()
        {
            var notSentDocuments = await Database.GetList<ConsignmentDocument>(x => !x.IsSent);

            var consignments = notSentDocuments
                .Where(x => x.File.FileExtension == ".pdf" && !AsmFileHelper.IsX2(x.Name))
                .GroupBy(
                    x => x.Consignment,
                    x => x,
                    (key, g) => new { con = key, docs = g.ToList() }
                ).ToList();

            foreach (var consignment in consignments)
                await EmailTemplate.SendEADDeliveryDocuments(consignment.con, consignment.docs);

            await Database.Update(notSentDocuments, x => x.IsSent = true);
        }

    }
}
