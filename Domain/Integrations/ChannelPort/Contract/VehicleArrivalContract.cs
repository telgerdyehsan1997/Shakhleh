using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class VehicleArrivalContract : BaseContract
    {
        public VehicleArrivalContract()
        {
            Lrns = new List<string>();
        }
        public List<string> Lrns { get; set; }

        public override void Validate()
        {
            if (Lrns.None())
                throw new ValidationException("All fields must have value.");
        }
    }
}