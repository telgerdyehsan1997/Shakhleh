using MSharp;

namespace Domain
{
    class Carrier : EntityType
    {
        public Carrier()
        {
            this.Archivable();
            String("Name").Mandatory();
            Associate<User>("Created by").DatabaseIndex();
            Associate<Company>("Company").DatabaseIndex();
            Associate<Country>("Country").Mandatory().DatabaseIndex();
            String("Postcode").Mandatory();
            String("Address line 1").Mandatory();
            String("Address line 2");
            String("Town/City").Name("Town").Mandatory();
            String("EORI number").Mandatory();
            String("Address").CalculatedFrom("new string[] { AddressLine1, AddressLine2, Town, Postcode }.Trim().ToString(\", \")");
            Bool("Is created from API").Mandatory().DatabaseIndex();

            ToStringExpression("GetDisplayText()");


        }
    }
}