using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel_Ports_Test.Constants
{
    public class CarrierFactory
    {
        private Carrier _Amazon { get; set; }
        public Carrier CreateCarrierAmazon()
        {
            _Amazon = _Amazon ?? new Carrier()
            {
                Name = "Amazon",
                Country = "GB - United Kingdom",
                Postcode = "TW1 123",
                AddressLine1 = "Twickenham Road",
                AddressLine2 = "Eel Pie Island",
                Town = "London",
                EORINumber = "GB347051400262"
            };
            return _Amazon;
        }
    }
}
