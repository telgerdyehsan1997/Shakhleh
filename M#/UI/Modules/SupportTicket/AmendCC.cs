using MSharp;

namespace Modules
{
    class AmendCC : FormModule<Domain.SupportTicket>
    {
        public AmendCC()
        {
            HeaderText("CC's");

            var cc = Box("CC", BoxTemplate.WrapperDiv);
            MasterDetail(x => x.EmailCC, a =>
            {
                a.Field(x => x.EmailCc)
                .NoLabel();

                a.Button("Add Another")
                .NoText()
                .Icon(FA.Plus)
                .CausesValidation(false)
                .CssClass("add-button")
                .OnClick(x => x.AddMasterDetailRow());
            })
            .InitialCardinality(1)
            .Label("CC")
            .Box(cc);
           
            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save")
             .IsDefault()
             .Icon(FA.Check)
            .OnClick(x =>
            {
                x.CSharp(@"foreach (var email in info.EmailCC)
                {
                  if(email.EmailCc.HasValue())
                       if (!Helper.EmailIsValid(email.EmailCc))
                            return Notify(email.EmailCc + "" is not a valid Email address"");

                }");
                x.CSharp(@"info.EmailCC = info.EmailCC.Except(x => x.EmailCc.IsEmpty()).ToList();");
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.CloseModal(Refresh.Full);
            });
        }
    }
}