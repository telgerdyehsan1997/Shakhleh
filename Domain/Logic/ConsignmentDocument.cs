namespace Domain
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Olive;
    using Olive.Entities;
    using System.Security.Principal;

    partial class ConsignmentDocument
    {


        protected override async Task OnSaved(SaveEventArgs e)
        {
            await base.OnSaved(e);

            if (IsManual) return;

            if (File.FileExtension == ".pdf" && !AsmFileHelper.IsX2(Name))
                await EmailTemplate.SendEADDeliveryDocument(Consignment, File);
        }

        public static async Task SendX2ToClient(Consignment consignment, Blob file)
        {
            if (consignment.Route.IsAnyOf("6"))
                await EmailTemplate.SendEADDeliveryDocument(consignment, file, isX2: true, isCustom: false);
            else
                await EmailTemplate.SendEADDeliveryDocument(consignment, file, isX2: true, isCustom: true);
        }

    

        public bool IsFileVisibleTo(IPrincipal user) => true;

        public bool IsAttachmentVisibleTo(IPrincipal user) => true;
    }
}