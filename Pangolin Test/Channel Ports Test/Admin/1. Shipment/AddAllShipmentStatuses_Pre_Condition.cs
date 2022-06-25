using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAllShipmentStatuses_Pre_Condition : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddRoutePortsmouthToCalais, AdminAddsCompanyTruckersLtd, CreateNewCompanyUser_JohnSmith, AddNewContactGroup_Import, EditTruckersToBeForwarder, AdminAddsProduct_IPad>();
            //this test case is not designed to run by istelf.
            //see AddAllShipmentTypesForDetails
            LoginAs<ChannelPortsAdmin>();



            string[] References = new string[] {
                "Draft",
                "ReadyToTransmit",
                "ReadyToTrAPI",
                "ASMAccept",
                "ASMReject",
                "AwaitingArrival",
                "AwaitingDeparture",
                "ProcessingErrorArrival",
                "ProcessingErrorDeparture",
                "Arrived",
                "WithCustoms",
                "QueriedArrived",
                "QueriedWithCustoms",
                "Cleared",
                "Cancelled",
                "MGenereal",
                "MCPC",
                "MLicense",
                "MRoute",
                "MGenerealASMAccepted",
                "MCPCASMAccepted",
                "MLicenseASMAccepted",
                "MRouteASMAccepted",
                "MGenerealASMRejected",
                "MCPCASMRejected",
                "MLicenseASMRejected",
                "MRouteASMRejected",
                "InternalError",
                "Partial"};


            ExpectHeader("Shipments");


            for (int i = 0; i < References.Length; i++)
            {


                ClickLink("New Shipment");

                WaitToSeeHeader("Shipment Details");

                ClickField("Company name");
                System.Threading.Thread.Sleep(1000);
                Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
                AtLabel("Company type").Expect("Forwarder");
                ClickField("Primary contact");
                System.Threading.Thread.Sleep(1000);
                Expect(What.Contains, "JOHN SMITH");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "JOHN SMITH");

                ClickLabel("Group");
                Above("Customer Reference").Click("---Select---");
                WaitToSeeText("Import");
                Press(Keys.ArrowDown);
                Press(Keys.Enter);
                Set("Customer Reference").To(References[i].Substring(0, Math.Min(References[i].Length, 16)));
                ClickLabel("Into UK");

                Set("Vehicle number").To("T37");
                Set("Trailer number").To("T87");

                ClickField("Route");
                System.Threading.Thread.Sleep(1000);
                Expect(What.Contains, "CALAIS to Portsmouth");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "CALAIS to Portsmouth");
                ClickButton(That.Contains, "Save and Add/Amend Consignments");
                System.Threading.Thread.Sleep(1000);
                Type("04/10/2022");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "Save");


                ExpectHeader("Consignment Details");

                ClickField("UK trader");
                System.Threading.Thread.Sleep(1000);
                Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

                ClickField("Partner name");
                System.Threading.Thread.Sleep(1000);
                Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

                Set("Declarant").To("");
                ClickHeader("Consignment Details");
                ClickLabel("Declarant");
                System.Threading.Thread.Sleep(1000);
                Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

                Set("Total packages").To("4");
                Set("Total gross weight").To("5.26");
                Set("Total net weight").To("4.992");
                Set("Invoice number").To("TRUCKERS-2019-1101");

                ClickLabel("Invoice currency");
                System.Threading.Thread.Sleep(1000);
                Expect(What.Contains, "Great Britain - GBP");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "Great Britain - GBP");
                Set("Total value").To("500");
                AtLabel("Only 1 Commodity").ClickLabel("Yes");
                Set("Terms of sale").To("FAS - Free Alongside Ship");
                Click(What.Contains, "Save and Add Commodities");

                ExpectHeader("Commodity Details");

                ClickLabel("Product code");
                System.Threading.Thread.Sleep(1000);
                Expect(What.Contains, "IPAD - ABS12343");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "IPAD - ABS12343");
                AtField("Gross weight").ExpectValue("5.26");
                AtField("Net weight").Expect("4.992");
                RightOf(The.Top, "Second quantity").ExpectText(That.Contains, "025 Litres");
                Set("Second quantity").To("100");

                ClickLabel("Country of origin");
                System.Threading.Thread.Sleep(1000);
                Expect(What.Contains, "GR - Greece");
                System.Threading.Thread.Sleep(1000);
                Click(What.Contains, "GR - Greece");

                AtLabel("Preference").ClickLabel("Yes");
                AtLabel("Preference type").ClickLabel("Invoice declaration");
                Set("Number of packages for this commodity code (if known)").To("2");
                Click("Save");

                Click("Shipments");
                ExpectHeader("Shipments");


                //Click("Tracking Number");
                //Click("Tracking Number");
                //for (int j = 0; j < i; j++)
                //{
                //    ExpectRow(That.Contains, "R07190000" + (j + 1).ToString("00"));
                //}
            }
        }
    }
}
