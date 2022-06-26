using MSharp;

namespace Domain
{
    public class AdminUser : SubType<User>
    {
        public AdminUser()
        {
            this.Archivable();

            String("Impersonation token", 40);
            Bool("Is admin").Mandatory(value: false);
        }
    }
}