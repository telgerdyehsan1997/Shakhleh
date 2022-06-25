using MSharp;

namespace Domain
{
    public class CompanyUser : SubType<User>
    {
        public CompanyUser()
        {
            this.Archivable();

            String("Telephone number").Accepts(TextPattern.IntegerText_digitsOnly);
            BigString("Notes");
            Associate<Company>("Company").DatabaseIndex();
            Bool("Accounts department").Mandatory();
            Bool("Is admin").Mandatory();
            Bool("Recieves CFSP report").Mandatory();

        }
    }
}
