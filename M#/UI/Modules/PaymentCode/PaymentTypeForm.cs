﻿using MSharp;

namespace Modules
{
    class PaymentTypeForm : FormModule<Domain.PaymentType>
    {
        public PaymentTypeForm()
        {
            HeaderText("Payment Type Details");

            Field(x => x.Code);
            Field(x => x.Description);

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });
        }
    }
}