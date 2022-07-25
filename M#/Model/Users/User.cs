using MSharp;

namespace Domain
{
    public class User : EntityType
    {
        public User()
        {
            Abstract();
            
            String("First name").Mandatory();
            String("Last name").Mandatory();
            String("Name").Calculated().Getter("FirstName + \" \" + LastName");
            String("Email", 100).Unique().Accepts(TextPattern.EmailAddress);
            String("Phone", 20).Unique();
            String("Password", 100).HashPassword().SaltProperty("Salt").Accepts(TextPattern.Password);
            String("Salt");
            this.Archivable();
        }
    }
}