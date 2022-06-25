using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public abstract class BaseContract
    {
        public Guid? OriginalId { get; set; }

        public abstract void Validate();
    }
}
