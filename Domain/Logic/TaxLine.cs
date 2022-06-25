namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;

    partial class TaxLine
    {
        public bool Isa => this.TaxType.ToLower().Equals("a");
    }

}