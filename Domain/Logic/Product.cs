namespace Domain
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Olive;
    using Olive.Security;
    using Olive.Entities;
    using Olive.Entities.Data;
    using System.Text.RegularExpressions;
    using Domain.AEB.DTOs;

    partial class Product
    {

        protected async override Task OnValidating(EventArgs e)
        {
            if (Licenced == false && ExportLicence.HasValue())
            {
                throw new ValidationException("This product cannot have export licence if it is not licenced");
            }
            if (CompanyId.HasValue && !IsCreatedFromAPI && Code.HasValue())
            {
                // Find an existing Product with the same Code and Company

                if (await Database.Any<Product>(p => p != this && p.Code == Code && p.CompanyId == CompanyId && p.IsCreatedFromAPI == false))
                {
                    throw new ValidationException("There is an existing Product with the same Code and Company in the database already.");
                }
            }
            if (IsNew)
                Code = Helper.UniqueId();
        }

        // Ensure uniqueness of Code and Company for UI Products
        public static Task<Product> FindByCompanyAndCode(Domain.Company company, string code)
        {
            return Database.FirstOrDefault<Product>(p => p.CompanyId == company && p.Code == code);
        }

        public async Task<Product> GetProductForApi()
        {
            var filter = await Database.Of<Product>().Where(x =>
                x.Name == Name &&
                x.CompanyId == Company &&
                x.CommodityCodeId == CommodityCode &&
                x.VATId == VAT &&
                x.IsCreatedFromAPI
            )
                .FirstOrDefault();

            return filter ?? this;
        }
    }
}
