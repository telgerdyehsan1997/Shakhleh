using Pangolin;

namespace Channel_Ports_Test
{
    public static class LogInHelper
    {
        public static void LogInAsGivenUser(this UITest @this, string email, string password, string date = "01/07/2021", string time = "12:00")
        {
            @this.Logout();
            @this.AssumeDate(date);
            @this.AssumeTime(time);
            @this.Goto("/");
            @this.Set("Email").To(email);
            @this.Set("Password").To(password);
            @this.ClickButton("Login");

            // To Throw error if credentials are incorrect.
            @this.ExpectNo("Invalid email and/or password. Please try again.");
        }
    }

    public class GvmsUser_GvmsOnly : UITest
    {
        public override void RunTest()
        {
            this.LogInAsGivenUser("gvms.user@uat.co", "test");
        }
    }

    public class RobertJones_Undischarged : UITest
    {
        public override void RunTest()
        {
            this.LogInAsGivenUser("robert.jones@uat.co", "test");
        }
    }

    public class ChannelPorts_Undischarged : UITest
    {
        public override void RunTest()
        {
            this.LogInAsGivenUser("undischargedadmin@uat.co", "test");
        }
    }

    public class Goro_MajimaConstruction : UITest
    {
        public override void RunTest()
        {
            this.LogInAsGivenUser("goro.majima@uat.co", "test");
        }
    }
}