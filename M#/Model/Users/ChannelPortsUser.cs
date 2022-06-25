using MSharp;

namespace Domain
{
    public class ChannelPortsUser : SubType<User>
    {
        public ChannelPortsUser()
        {
            this.Archivable();

            String("Impersonation token", 40);
            Bool("Is admin").Mandatory(value: false);
        }
    }
}