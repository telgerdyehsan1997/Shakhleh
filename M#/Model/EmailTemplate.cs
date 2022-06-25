using MSharp;

namespace Domain
{
    public class EmailTemplate : EntityType
    {
        public EmailTemplate()
        {
            Implements("Olive.Email.IEmailTemplate");
            InstanceAccessors("RecoverPassword", "PasswordHasBeenReset", "WelcomeEmail", "ShipmentSubmission", "Control",
                              "TransitDocument", "RouteDocumentsEmail", "X2EADDocumentDelivery", "EADDocumentDelivery", "NCTSDocumentDelivery",
                              "ASMAcceptedClientNotification", "CommodityCodeControlEmail", "CustomsIntervention",
                              "OnHoldDuetoValue", "ConsignmentInDraftForMoreThan7Days", "DraftConsignmentArchived",
                              "StatusNotificationEmail", "ErrorWhileTransitASM", "ArchiveNotification", "UnarchiveNotification", "ManualQuota",
                              "InventoryUsed", "SendCopyEntry", "RaisedTicket", "ClosedTicket", "ResponsesTicket", "ShipmentFileDelivery",
                              "CFSPMonthlyReport");

            String("Key").Mandatory().Unique();
            String("Name").Mandatory();
            String("Subject").Mandatory();
            BigString("Body", 10).Mandatory();
            String("Mandatory placeholders");
            DateTime("Date email sent");
            DefaultToString = Property("Key");
        }
    }
}