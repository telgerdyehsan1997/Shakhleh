using MSharp;

namespace Domain
{
    class Shipment : EntityType
    {
        public Shipment()
        {
            this.Archivable();

            //InverseAssociate<ShipmentTransitOffice>("Border crossing", "Shipment").MaxCardinality(4);

            Associate<TransitOffice>("Office of destination");
            Associate<TransitOffice>("First border crossing");
            Associate<TransitOffice>("Second border crossing");
            Associate<TransitOffice>("Third border crossing");
            Associate<TransitOffice>("Fourth border crossing");

            DefaultToString = String("Tracking number").Max(11).MinLength(11).Unique().DatabaseIndex();
            DateTime("Date").HasTime(false).Mandatory().Default("c#: LocalTime.Now").DatabaseIndex();

            var company = Associate<Company>("Company").Mandatory().DatabaseIndex();
            Associate<Person>("Primary contact").Mandatory();
            Associate<NotifyType>("Notify additional party").Mandatory().Default("c#: NotifyType.NotRequired");
            Associate<ContactGroup>("Group");
            AssociateManyToMany<Person>("Contact name");
            var referece = String("My reference for CP invoice").Max(16).DatabaseIndex();
            Associate<ShipmentType>("Type").Mandatory().Default("c#:ShipmentType.IntoUk");
            String("Vehicle number").Max(13).MinLength(3).DatabaseIndex();
            String("Trailer number").Max(13).MinLength(3).DatabaseIndex();
            InverseAssociate<UploadAttachment>("Upload attachments", "Shipment");
            InverseAssociate<Consignment>("Consignments", "Shipment");
            Date("Expected date").Mandatory().DatabaseIndex();
            Date("Arrival date");
            //var port = Associate<Port>("Port of arrival");
            Associate<Progress>("Progress").Mandatory().Default("c#:Progress.Draft").DatabaseIndex();
            Bool("Is NCTSShipmentOut convertible").Mandatory(value: false).DatabaseIndex();
            Bool("Is GVMS").Mandatory(value: false);
            Associate<GVMSStatus>("GVMS status");
            Associate<Country>("Driver mobile country");
            String("Driver mobile number");
            Associate<Route>("Route");

            String("Route name").Calculated().CalculatedFrom("GetRouteName()");

            Bool("Use authorised location?").Mandatory(value: false).Default(cs("false"));
            Associate<AuthorisedLocation>("Authorised location");
            //  Associate<GuaranteeLength>("Guarantee length");

            Bool("IsAPI").Mandatory();
            Bool("IsDraft").Mandatory();

            DateTime("Consignments cleared date");
            Bool("Safety and security").Mandatory(value: false);
            String("Container Number");

            UniqueCombination(new[] { "Company", "My reference for CP invoice" });
            Bool("GVMApiProcessed").Mandatory();

            String("Notification boxId");
            String("Notification messageId");
            String("GVMSERN");

            Bool("Is weights mismatch").Mandatory();
            Bool("Unaccompanied").Mandatory();

            Associate<Carrier>("Carrier");

            //GVMS systems is not required currently.
            //Bool("GVMS").Mandatory().Default("false");
            //Bool("Are there MRNs").Mandatory().Default("false");
            //Bool("Are MRNs available now").Mandatory().Default("false");
            //String("MRN");
            //Bool("Unaccompanied trailer").Mandatory().Default("false");
        }
    }
}