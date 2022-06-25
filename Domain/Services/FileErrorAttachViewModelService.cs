using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IFileErrorAttachViewModelService
    {
        Task<IEnumerable<Consignment>> GetConsignments(string identifier);
    }
    public class FileErrorAttachViewModelService : IFileErrorAttachViewModelService
    {

        private readonly IDatabase Database = Context.Current.Database();

        public async Task<IEnumerable<Consignment>> GetConsignments(string identifier)
        {
            var result = await Database.Of<Consignment>().GetList();
            if (identifier.HasValue())
                return result.Where(x => x.UCR?.Contains(identifier) == true || x.ConsignmentNumber?.Contains(identifier) == true);
            return result;
        }
    }
}
