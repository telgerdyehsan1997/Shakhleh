using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vm = ViewModel;

namespace Controllers
{
    partial class AddProductController
    {
        public async Task CreateNewProducts(vm.CommodityProductForm info)
        {
            var companies = new List<Company>
            {
                info.Consignment.UKTrader,
                info.Consignment.Partner,
                info.Consignment.Declarant,
                info.Consignment.Shipment.Company
            }.Distinct()
            .ToList();

            foreach (var company in companies)
            {
                var product = new Product
                {
                    Name = info.Name,
                    Company = company,
                    CommodityCode = info.CommodityCode,
                    VAT = info.VAT ?? (await info.CommodityCode.GetSOrFirst()),
                };

                await Database.Save(product);
            }
        }
    }
}
