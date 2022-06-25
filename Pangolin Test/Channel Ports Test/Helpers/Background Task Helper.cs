using Pangolin;

namespace Channel_Ports_Test
{
    public static class BackgroundTaskHelpers
    {
        public static void BroadcastMessage(this UITest @this)
        {
            @this.CheckBackgroundTasks();
            @this.AtRow("Send Broadcast Message").Column("Execute").Click("Execute");
            @this.WaitForNewPage();
            @this.Goto("/");
        }

        public static void GetExchangeRates(this UITest @this)
        {
            @this.CheckBackgroundTasks();
            @this.AtRow("Get Exchange Rate Month").Column("Execute").Click("Execute");
            @this.WaitForNewPage();
            //@this.Goto("/");
        }

        public static void ProcessFileStore(this UITest @this)
        {
            @this.CheckBackgroundTasks();
            @this.AtRow("Process File Store").Column("Execute").Click("Execute");
            @this.WaitForNewPage();
            //@this.Goto("/");
        }
    }
}