using Domain;
using Olive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace APIContracts
{
    public class TransitOfficeContract  
    {

        public string CountryCode { get; set; }
        public string CountryName { get; set; }

    }
}
