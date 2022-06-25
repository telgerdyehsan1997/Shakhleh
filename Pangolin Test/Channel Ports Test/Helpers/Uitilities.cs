using Pangolin;

namespace Channel_Ports_Test
{
    public static class Uitilities
    {
        public static void ClickAndWait(this UITest @this, string fieldName, string clickObject)
        {
            @this.ClickField(fieldName);
            System.Threading.Thread.Sleep(1000);
            @this.Expect(clickObject);
            System.Threading.Thread.Sleep(1000);
            @this.Click(clickObject);
        }
    }
}
