using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ReleaseShipmentContract : BaseContract
    {
        public string LRN { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
