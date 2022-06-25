using MSharp;

namespace Modules
{
    class InvoicePrintHeader : ViewModule<Domain.Invoice>
    {
        public InvoicePrintHeader()
        {
            IsViewComponent();
            RootCssClass("header");
            Markup(@"<div class=""channelports-details mb-4 p-3 border"">
       <div class=""details text-center"">
           <div class=""logo mb-2"">
               <img src=""/img/Logo.png"" alt=""""Logo"""">
           </div>
           <div class=""detail mb-2"">
               <h2>
                   ChannelPorts Ltd, Folkestone Services, Junction 11 M20, Hythe CT21 4BL
               </h2>
           </div>
           <div class=""detail mb-2"">
               <h2>
                  Invoice: @Model.Item.InvoiceNumber
               </h2>
           </div>
       </div>
       <div class=""row"">
           <div class=""col"">
               <div class=""contact"">
                   <div class=""item mb-2"">
                       Customer Support tel: 01304 272173
                   </div>
                   <div class=""item"">
                       Accounts tel: 01304 218329
                   </div>
               </div>
           </div>
           <div class=""col"">
               <div class=""contact"">
                   <div class=""item mb-2"">
                       Date/ Tax Point: @Model.Item.PrintDate.ToString(""d"")
                   </div>
                   <div class=""item mb-2"">
                       Account Number: @Model.Item.Company.CustomerAccountNumber
                   </div>
                   <div class=""item"">
                       Contact: @(Model.Item.Company.DepartmentName.Or(Model.Item.Company.PrimaryContact?.Name))
                   </div>
               </div>
           </div>
       </div>
   </div>
");
        }
    }
}