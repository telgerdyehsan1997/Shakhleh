using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompany_CFSPSetToOwn : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "CFSP Own Test";
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            this.NavigateToCompanies();

            //Creates the new Company
            ClickLink("New Company");
            ExpectHeader("Record Details");

            this.CreateNewCompany(
                companyType: CompanyTypeConstants.Customer,
                transactionTypes: new string[] { "Into uk" },
                accountNumber: "A9995",
                companyName: companyName,
                companyCountry: "GB - United Kingdom",
                postCode: "LND OA1",
                companyCity: "London",
                addressLine: "22 London Road",
                eoriNumber: "GB683470514001",
                gvmsType: GVMSConstants.Sometimes,
                safetyInbound: SecurityInboundConstants.Sometimes,
                safetyOutbound: SecurityOutboundConstants.Sometimes,
                representationType: "Direct",
                guarantorType: GuarantorType.None,
                cfspType: CFSPConstants.Own,
                eidrType: "Yes",
                cfspCpcNumber: "_0610040",
                sfdOnly: "No",
                authorisationNumber: "GB683470514002"
                );

            //Asserts that new Company has been created
            ExpectHeader("Companies");
            ExpectRow(companyName);
        }
    }
}