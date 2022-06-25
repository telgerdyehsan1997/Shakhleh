using MSharp;

namespace Domain
{
    public class Person : EntityType
    {
        public Person()
        {
            Abstract();

            String("First name").Mandatory();
            String("Last name").Mandatory();
            String("Name").Calculated().Getter("FirstName + \" \" + LastName");
            String("Email", 100).Mandatory().Accepts(TextPattern.EmailAddress);
            String("Mobile number").Accepts(TextPattern.IntegerText_digitsOnly);
            ToStringExpression("Name");
        }
    }
}