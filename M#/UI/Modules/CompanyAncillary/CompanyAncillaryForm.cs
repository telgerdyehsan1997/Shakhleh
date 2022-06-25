using MSharp;

namespace Modules
{
    class CompanyAncillaryForm : FormModule<Domain.Ancillary>
    {
        public CompanyAncillaryForm()
        {
            HeaderText("Ancillary Details");

            Field(x => x.Country)
                .ItemCssClass(@"@(""d-none"".OnlyWhen(!Model.Item.IsNew))")
                .ChangeEventHandler("await UpdateModel(info);")
                .AsDropDown();

            CustomField("CountryCode")
                .VisibleIf(CommonCriterion.IsEditMode_Item_IsNew)
                .Label("Country")
                .ControlMarkup("@info.CountryCode");

            Field(x => x.FreightChargePerTonne)
                .Mandatory()
                .VisibleIf("info.Country != null")
                .Label("Freight charge per tonne");

            Field(x => x.FullLoadFreightCharge)
                .Mandatory()
                .VisibleIf("info.Country != null")
                .Label("Full load freight charge");

            Field(x => x.ValueForVAT)
                .Mandatory()
                .VisibleIf("info.Country != null");

            AutoSet(x => x.Company).FromRequestParam("company");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });

            ViewModelProperty<string>("CountryCode")
                .OnBound("info.CountryCode = info.Item.Country?.Code;");

            OnControllerClassCode("fill data")
                .Code(@"async Task UpdateModel(vm.CompanyAncillaryForm info)
                {
                    var ancillary = await Database.FirstOrDefault<Ancillary>(a => a.CompanyId == null && a.CountryId == info.Country);
                    info.FreightChargePerTonne = ancillary?.FreightChargePerTonne;
                    info.FullLoadFreightCharge = ancillary?.FullLoadFreightCharge;
                    info.ValueForVAT = ancillary?.ValueForVAT;
                }");
        }
    }
}