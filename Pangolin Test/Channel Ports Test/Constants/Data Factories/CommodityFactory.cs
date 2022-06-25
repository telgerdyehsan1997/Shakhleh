using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel_Ports_Test.Constants
{
    public class CommodityFactory
    {
        private Commodity _MajimaCommodityOutOfUK { get; set; }
        public Commodity AddMajimaConstructionCommodityOutOfUK()
        {
            _MajimaCommodityOutOfUK = _MajimaCommodityOutOfUK ?? new Commodity()
            {
                Product = "MEAT",
                CommodityCode = "12345678",
                GrossWeight = "1",
                NetWeight = "1",
                SecondQuantity = "1",
                Value = "1",
                NumberOfPackages = "1",
                Country = "ES - Spain",
                AreGoodsLicencable = "No",
                HasHealthCertificateNumber = "No"
            };
            return _MajimaCommodityOutOfUK;
        }

        private Commodity _MajimaCommodityIntoUKSandS { get; set; }
        public Commodity AddMajimaConstructionCommodityIntoUKSandS()
        {
            _MajimaCommodityIntoUKSandS = _MajimaCommodityIntoUKSandS ?? new Commodity()
            {
                Product = "MEAT",
                CommodityCode = "12345678",
                GrossWeight = "1",
                NetWeight = "1",
                SecondQuantity = "1",
                Value = "1",
                NumberOfPackages = "1",
                Country = "GR - Greece",
                HasPreference = "Yes",
                PreferenceType = "Invoice declaration",
                AreGoodsLicencable = "No",
            };
            return _MajimaCommodityIntoUKSandS;
        }

        private Commodity _MajimaCommodityIntoUK { get; set; }
        public Commodity AddMajimaConstructionCommodityIntoUK()
        {
            _MajimaCommodityIntoUK = _MajimaCommodityIntoUK ?? new Commodity()
            {
                Product = "ADDITIONAL CODE",
                CommodityCode = "12345678",
                GrossWeight = "1",
                NetWeight = "1",
                SecondQuantity = "1",
                Value = "1",
                NumberOfPackages = "1",
                Country = "GR - Greece",
                HasPreference = "Yes",
                PreferenceType = "Invoice declaration",
                AreGoodsLicencable = "No"
            };
            return _MajimaCommodityIntoUK;
        }
        private Commodity _CFSPChannelPortsCommodityIntoUK { get; set; }
        public Commodity AddCFSPChannelPortsCommodityIntoUK()
        {
            _CFSPChannelPortsCommodityIntoUK = _CFSPChannelPortsCommodityIntoUK ?? new Commodity()
            {
                Product = "MONITORS",
                CommodityCode = "12345678",
                GrossWeight = "1",
                NetWeight = "1",
                SecondQuantity = "1",
                Value = "1",
                NumberOfPackages = "1",
                Country = "GR - Greece",
                HasPreference = "Yes",
                PreferenceType = "Invoice declaration",
                AreGoodsLicencable = "No"
            };
            return _CFSPChannelPortsCommodityIntoUK;
        }

        private Commodity  _UKCompanyCommodityIntoUK { get; set; }
        public Commodity AddUKCompnayCommodityIntoUK()
        {
            _UKCompanyCommodityIntoUK = _UKCompanyCommodityIntoUK ?? new Commodity()
            {
                Product = "KEYBOARD",
                CommodityCode = "12345678",
                GrossWeight = "1",
                NetWeight = "1",
                SecondQuantity = "1",
                Value = "1",
                NumberOfPackages = "1",
                Country = "GR - Greece",
                HasPreference = "No",
                PreferenceType = "Invoice declaration",
                AreGoodsLicencable = "No"
            };
            return _UKCompanyCommodityIntoUK;
        }

        private Commodity _RowCommodity { get; set; }
        public Commodity AddRowCommodity()
        {
            _RowCommodity = _RowCommodity ?? new Commodity()
            {

                Product = "ROW PRODUCT",
                CommodityCode = "1012100",
                GrossWeight = "1",
                NetWeight = "1",
                SecondQuantity = "1",
                Value = "1",
                NumberOfPackages = "1",
                Country = "JP - JAPAN",
                HasPreference = "No",
                AreGoodsLicencable = "No"
            };
            return _RowCommodity;
        }
    }
}
