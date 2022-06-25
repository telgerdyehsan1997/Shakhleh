using MSharp;

namespace Domain
{
    class Contact : SubType<Person>
    {
        public Contact()
        {
            this.Archivable();

            String("Telephone number").Accepts(TextPattern.IntegerText_digitsOnly);
            BigString("Notes");
            Associate<Company>("Company").DatabaseIndex();

        }
    }
}