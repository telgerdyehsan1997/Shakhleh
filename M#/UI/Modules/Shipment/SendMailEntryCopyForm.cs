using System;
using System.Collections.Generic;
using System.Text;
using MSharp;

namespace Modules
{
    class SendMailEntryCopyForm : FormModule<Domain.SendMailEntryCopyViewModel>
    {
        public SendMailEntryCopyForm()
        {
            HeaderText("Email Copy Documents");
            Header(@"<p style=""font-weight: bold;margin-bottom: 35px;font-style: italic;"">To enable the documents to be sent you will need to enter the declarants or UK Traders EORI number</p>");
            ViewModelProperty<Domain.Shipment>("Shipment").FromRequestParam("shipment");
            Inject("APIHandler.IEORIService");

            Field(x => x.EORINumber);

            Field(x => x.EmailAddress)
                .ControlCssClass("persist-casing");

            AutoSet(x => x.Shipment).Value("info.Shipment");
            
            Button("Cancel")
                .Text("Cancel")
                .OnClick(x => x.CloseModal());

            Button("Send")
                .Text("Send")
                .OnClick(x => {
                    x.CSharp(@"var emails = info.EmailAddress.Split("","");
                    foreach (var email in emails)
                    {
                        if (!Helper.EmailIsValid(email))
                        {
                            Notify(email + "" is not a valid Email address"");
                            return JsonActions(info);
                        };
                    }
                    if (!await info.Shipment.Company.IsEORINumberValid(info.Shipment,info.EORINumber))
                    {
                        Notify(info.EORINumber + "" is not a valid EORI number."");
                        return JsonActions(info);
                    }
                    ");
                    x.CSharp("await info.CopyDataTo(info.Item);");
                    x.CSharp(@"var isSend = await EmailTemplate.SendCopyShipmentDocument(info.Item);
                               if(!isSend){
                                  Notify(""No Entry Documents have been provided for this Shipment"");
                                }else{
                                  Notify(""Copy of Entry Documents has been successfully sent."");
                                }");
                    x.CloseModal(Refresh.Full);
                });

        }
    }
}
