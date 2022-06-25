using MSharp;

namespace Domain
{
    public class User : SubType<Person>
    {
        public User()
        {
            Abstract();

            String("Password", 100).HashPassword().SaltProperty("Salt").Accepts(TextPattern.Password);
            String("Salt");
        }
    }
}