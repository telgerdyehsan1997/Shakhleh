using Pangolin;

namespace Channel_Ports_Test
{
    public static class PortsHelper
    {
        public static void AddPort(this UITest @this, string portName, string portCountry, bool nonUk, string portCode, string nctsCode, string[] intoUkType,
            string intoUkValue, string outOfUkType, string outOfUkValue, string usePortCode = null, string locationCode = null, string ukbfEmail = null,
            string dtiBadge = null)
        {
            @this.ClickLink("New Port");
            @this.ExpectHeader("Port Details");

            //Sets the Port Details
            @this.Set("Port name").To(portName);
            @this.Set("Country").To(portCountry);
            if (nonUk == true)
            {
                @this.AtLabel("Non-UK").ClickLabel("Yes");
            }
            else
            {
                @this.AtLabel("Non-UK").ClickLabel("No");
                @this.ExpectField("Location Code");
                @this.ExpectField("UKBF email");
            }

            @this.Set("Port code").To(portCode);
            @this.ClickField("NCTS code");
            System.Threading.Thread.Sleep(1000);
            @this.Expect(nctsCode);
            System.Threading.Thread.Sleep(1000);
            @this.Click(nctsCode);

            foreach (var intoType in intoUkType)
                @this.AtLabel("Into UK Type").ClickLabel(intoType);

            @this.Set("Into UK Value").To(intoUkValue);
            @this.AtLabel("Out Of UK Type").ClickLabel(outOfUkType);
            @this.Set("Out of UK Value").To(outOfUkValue);
            @this.ClickButton("Save");

            //Asserts that Port has been saved
            @this.ExpectRow(portName);
        }
    }
}