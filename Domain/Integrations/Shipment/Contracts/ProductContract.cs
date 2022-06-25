using Olive;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace APIContracts
{
    public class ProductContract
    {
        public string ProductCode { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CommodityImportCode { get; set; }
        [Required]
        public string CommodityExportCode { get; set; }
    }
}
