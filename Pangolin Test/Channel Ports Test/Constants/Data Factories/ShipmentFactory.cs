using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test.Constants
{
    public class ShipmentFactory
    {
        private Shipment _MajimaShipmentOutOfUK { get; set; }
        public Shipment CreateMajimaConstructionShipmentOutOfUK()
        {
            _MajimaShipmentOutOfUK = _MajimaShipmentOutOfUK ?? new Shipment()
            {
                CompanyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567",
                Type = "Out of UK",
                IsNCTS = "No",
                Route = "BLACKPOOL TO CALAIS",
                IsSafetyAndSecurity = "No",
                PrimaryContact = "KAZUMA KIRYU",
                CustomerReference = "CustomerRef1",
                VehicleNumber = "12345",
                IsUnaccompanied = "No",
                ExpectedDate = "01/07/2021",
                TrackingNumber = "T0721000001"

            };
            return _MajimaShipmentOutOfUK;
        }

        private Shipment _SAndSShipment { get; set; }
        public Shipment MajimaConstrunctionSafetyAndSecurity()
        {
            _SAndSShipment = _SAndSShipment ?? new Shipment()
            {
                CompanyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567",
                Type = ShipmentTypeConstants.OutUK,
                Route = "Blackpool to CALAIS",
                IsSafetyAndSecurity = "Yes",
                PrimaryContact = "Kazuma Kiryu",
                CustomerReference = "SAndATest",
                VehicleNumber = "12345",
                IsUnaccompanied = "No",
                ExpectedDate = "02/05/2022",
                IsNCTS = "No",
                TrackingNumber = "T0721000001"
            };
            return _SAndSShipment;
        }

        private Shipment _CFSPShipment { get; set; }
        public Shipment CreateCFSPShipment()
        {
            _CFSPShipment = _CFSPShipment ?? new Shipment()
            {
                CompanyName = "CFSP OWN TEST - LONDON - LND OA1 - GB683470514001",
                Type = ShipmentTypeConstants.IntoUK,
                Route = "CALAIS to Blackpool",
                PrimaryContact = "CFSP CONTACT",
                NotifyAdditionalParties = AdditionalPartyConstants.NotRequired,
                CustomerReference = "SFDTest",
                IsSafetyAndSecurity = "No",
                VehicleNumber = "123456789",
                ExpectedDate = "02/05/2022",
                TrackingNumber = "R0721000001"
            };
            return _CFSPShipment;
        }

        private Shipment _CFSPShipmentINVPort { get; set; }
        public Shipment CreateCFSPShipmentINVPort()
        {
            _CFSPShipmentINVPort = _CFSPShipmentINVPort ?? new Shipment()
            {
                CompanyName = "CFSP OWN TEST - LONDON - LND OA1 - GB683470514001",
                Type = ShipmentTypeConstants.IntoUK,
                Route = "CALAIS to BARKING RAIL TERMINAL",
                PrimaryContact = "CFSP CONTACT",
                NotifyAdditionalParties = AdditionalPartyConstants.NotRequired,
                CustomerReference = "SFDTest",
                IsSafetyAndSecurity = "No",
                VehicleNumber = "123456789",
                ExpectedDate = "02/05/2022",
                TrackingNumber = "R0721000001"
            };
            return _CFSPShipmentINVPort;
        }

        private Shipment _ChannelPortsCFSPShipment { get; set; }
        public Shipment CreateChannelPortsCFSPShipment()
        {
            _ChannelPortsCFSPShipment = _ChannelPortsCFSPShipment ?? new Shipment()
            {
                CompanyName = "CFSP CHANNELPORTS TEST - LONDON - LND OA1 - GB683470514001",
                Type = ShipmentTypeConstants.IntoUK,
                Route = "CALAIS to Blackpool",
                PrimaryContact = "CFSP CONTACT",
                NotifyAdditionalParties = AdditionalPartyConstants.NotRequired,
                CustomerReference = "SFDTest",
                IsSafetyAndSecurity = "No",
                VehicleNumber = "123456789",
                ExpectedDate = "02/05/2022",
                TrackingNumber = "R0721000001"
            };
            return _ChannelPortsCFSPShipment;
        }

        private Shipment _SAndSShipmentIntoUK { get; set; }
        public Shipment MajimaConstrunctionSafetyAndSecurityIntoUK()
        {
            _SAndSShipmentIntoUK = _SAndSShipmentIntoUK ?? new Shipment()
            {
                CompanyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567",
                Type = ShipmentTypeConstants.IntoUK,
                Route = "CALAIS to Blackpool",
                IsSafetyAndSecurity = "Yes",
                PrimaryContact = "Kazuma Kiryu",
                CustomerReference = "SAndAIntoUK",
                VehicleNumber = "12345",
                IsUnaccompanied = "No",
                ExpectedDate = "02/05/2022",
                ContainerNumber = "123456",
                Carrier = "AMAZON - LONDON - TW1 123 - GB347051400262",
                TrackingNumber = "R0721000001"
            };
            return _SAndSShipmentIntoUK;
        }

        private Shipment _MajimaShipmentIntoUK { get; set; }
        public Shipment CreateMajimaConstructionShipmentIntoUK()
        {
            _MajimaShipmentIntoUK = _MajimaShipmentIntoUK ?? new Shipment()
            {
                CompanyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567",
                Type = ShipmentTypeConstants.IntoUK,
                Route = "CALAIS to Blackpool",
                IsSafetyAndSecurity = "No",
                PrimaryContact = "Kazuma Kiryu",
                CustomerReference = "IntoUK1",
                VehicleNumber = "12345",
                IsUnaccompanied = "No",
                ExpectedDate = "02/05/2022",
                TrackingNumber = "R0721000001"
            };
            return _MajimaShipmentIntoUK;
        }

        private Shipment _UKCompanyShipmentIntoUK { get; set; }
        public Shipment CreateUKCompanyShipmentIntoUK()
        {
            _UKCompanyShipmentIntoUK = _UKCompanyShipmentIntoUK ?? new Shipment()
            {
                CompanyName = "UK COMPANY - TWICKENHAM - TW1 3DY - GB683470514002 - 2345678",
                Type = ShipmentTypeConstants.IntoUK,
                Route = "CALAIS to Blackpool",
                IsSafetyAndSecurity = "No",
                PrimaryContact = "Jim Milton",
                CustomerReference = "IntoUK1",
                VehicleNumber = "12345",
                IsUnaccompanied = "No",
                ExpectedDate = "02/05/2022",
                TrackingNumber = "R0721000001"
            };
            return _UKCompanyShipmentIntoUK;
        }

        private Shipment _RoWShipmentIntoUK { get; set; }
        public Shipment CreateRoWShipmentIntoUK()
        {
            _UKCompanyShipmentIntoUK = _UKCompanyShipmentIntoUK ?? new Shipment()
            {
                CompanyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567",
                Type = ShipmentTypeConstants.IntoUK,
                Route = "CALAIS to Blackpool",
                IsSafetyAndSecurity = "No",
                PrimaryContact = "Kazuma Kiryu",
                CustomerReference = "RoWNoPrefNoEU",
                VehicleNumber = "12345",
                IsUnaccompanied = "No",
                ExpectedDate = "02/05/2022",
                TrackingNumber = "R0721000001"
            };
            return _UKCompanyShipmentIntoUK;
        }
    }
}

