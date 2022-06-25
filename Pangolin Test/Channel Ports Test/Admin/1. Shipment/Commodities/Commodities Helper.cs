using Pangolin;
using System;

namespace Channel_Ports_Test
{
    public static class CommoditiesHelper
    {

        public static void NavigateToShipmentCommodities(this UITest @this, string trackingNumber, string consignmentNumber)
        {
            @this.AtRow(trackingNumber).Column("Edit").Click("Edit");
            @this.ExpectHeader("Shipment Details");
            @this.ClickHeader("Shipment Details");
            @this.Click("Save and Add/Amend Consignments");
            @this.AtRow(consignmentNumber).Column("Edit").Click("Edit");
            @this.ExpectHeader("Consignment Details");
            @this.Click("Save and Add Commodities");
        }
        public static void AddProductCode(this UITest @this, string productCode, string productName, string productCommodityCode)
        {
            @this.Click("AddProduct");
            @this.ExpectHeader("Product Details");
            @this.Set("Code").To(productCode);
            @this.Set("Name").To(productName);
            @this.Set("Commodity code").To(productCommodityCode);
            @this.ClickHeader("Product Details");
            @this.ClickField("Commodity code");
            @this.Click(productCommodityCode);
            @this.ClickButton(The.Top, "Save");
        }

        public static void AddCommodityDetails(this UITest @this, string productSelection, string productCode, string grossWeight, string netWeight,
            string commodityValue, string numberOfPackages, string commodityCountry, bool commodityOut, string countryPreference = null, string hazardousGoods = null,
            string unCode = null, string hasHealthCertificateNumber = null, string healthCertificateNumber = null, string healthCertificateCode = null, string secondQuantity = null)
        {
            @this.ExpectHeader("Commodity Details");
            @this.ClickHeader("Commodity Details");
            @this.ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(The.Top, productSelection);
            System.Threading.Thread.Sleep(1000);
            @this.Click(The.Top, productSelection);

            @this.Set("Gross weight").To(grossWeight);
            @this.Set("Net weight").To(netWeight);
            @this.Set("Value").To(commodityValue);

            if (secondQuantity.HasAny())
            {
                @this.Set("Second quantity").To(secondQuantity);
            }

            @this.Set("Number of packages for this commodity code (if known)").To(numberOfPackages);

            if (commodityOut == true)
            {
                @this.ClickField("Country of destination");
                System.Threading.Thread.Sleep(1000);
                @this.Expect(What.Contains, commodityCountry);
                System.Threading.Thread.Sleep(1000);
                @this.Click(What.Contains, commodityCountry);
            }
            else
            {
                @this.ClickField("Country of origin");
                System.Threading.Thread.Sleep(1000);
                @this.Expect(What.Contains, commodityCountry);
                System.Threading.Thread.Sleep(1000);
                @this.Click(What.Contains, commodityCountry);
            }

            if (countryPreference.Contains("Yes"))
            {
                @this.AtLabel("Preference").ClickLabel("Yes");
                @this.AtLabel("preference type").ClickLabel("Invoice declaration");
            }
            else
            {
                @this.ClickLabel("No");
            }

            if (hazardousGoods.HasAny())
            {
                @this.ExpectLabel("Are the goods hazardous?");
                if (hazardousGoods.Contains("Yes"))
                {
                    @this.AtLabel("Are the good hazardous?").ClickLabel("Yes");
                }
                if (hazardousGoods.Contains("No"))
                {
                    @this.AtLabel("Are the good hazardous?").ClickLabel("No");
                }
                else
                    @this.ExpectNo("Are the good hazardous?");
            }

            if (hasHealthCertificateNumber.HasAny())
            {
                @this.ExpectLabel("Have Health Certificate Number");
                if (hasHealthCertificateNumber.Contains("Yes"))
                {
                    @this.AtLabel("Have Health Certificate Number").ClickLabel("Yes");
                    @this.ExpectLabel("Health Certificate Number");
                    @this.Set("Health Certificate Number").To(healthCertificateNumber);
                    @this.ExpectLabel("Health Certificate Code");
                    @this.AtLabel("Health Certificate Code").ClickLabel(healthCertificateCode);
                }
                else if (hasHealthCertificateNumber.Contains("No"))
                {
                    @this.AtLabel("Have Health Certificate Number").ClickLabel("No");
                }
                else
                    @this.ExpectNo("Health Certificate Number");
            }

            @this.ClickButton("Save");
            @this.ExpectRow(productCode);


        }

        private static void CreateMultipleCommodities(this UITest @this, int commodityNo, string productSelection, string productCode, string grossWeight, string netWeight, string commodityValue, string numberOfPackages, string countryOfOrigin, string countryPreference = null)
        {
            for (int i = 0; i <= commodityNo; i++)
            {
                @this.ClickField("Product code");
                System.Threading.Thread.Sleep(1000);
                @this.Expect(The.Top, productSelection);
                System.Threading.Thread.Sleep(1000);
                @this.Click(The.Top, productSelection);

                @this.Set("Gross weight").To(grossWeight);
                @this.Set("Net weight").To(netWeight);
                @this.Set("Value").To(commodityValue);
                @this.Set("Number of packages for this commodity code (if known)").To(numberOfPackages);

                @this.ClickField("Country of origin");
                System.Threading.Thread.Sleep(1000);
                @this.Expect(What.Contains, countryOfOrigin);
                System.Threading.Thread.Sleep(1000);
                @this.Click(What.Contains, countryOfOrigin);

                if (countryPreference.Contains("Yes"))
                {
                    @this.AtLabel("Preference").ClickLabel("Yes");
                }

                @this.ClickButton("Save");
                @this.ExpectRow(productCode);
            }
        }

        public static void AddSFDCommodity(this UITest @this, string countryOfOrigin, string descriptionOfGoods)
        {
            @this.ClickLink("New Commodity");
            @this.ExpectHeader("Commodity Details");
            @this.ClickHeader("Commodity Details");
            System.Threading.Thread.Sleep(1000);
            @this.ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(countryOfOrigin);
            System.Threading.Thread.Sleep(1000);
            @this.Click(countryOfOrigin);
            @this.Set("Description of goods").To(descriptionOfGoods);
            @this.ClickButton("Save");
            @this.Expect("Complete");
        }
    }
}