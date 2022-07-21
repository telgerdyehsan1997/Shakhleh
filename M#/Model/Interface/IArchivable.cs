using MSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    class IArchivable : EntityType
    {
        public IArchivable()
        {
            DatabaseMode(DatabaseOption.Transient);
            IsInterface();
            Bool("IsDeactivated").Mandatory();
        }
    }
}
