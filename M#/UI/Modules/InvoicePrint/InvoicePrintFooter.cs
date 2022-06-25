using MSharp;

namespace Modules
{
    class InvoicePrintFooter : ViewModule<Domain.Invoice>
    {
        public InvoicePrintFooter()
        {
            Using("System.Globalization");
            IsViewComponent();
            RootCssClass("footer");
            Markup(@"
<div class=""channelports-details mt-4 border "">
      <div class=""details text-left p-3"">
          <div class=""row"">
              <div class=""col-6"">
                  <div class=""item mb-2"">
                      VAT/EORI GB 683 470 514 000
                  </div>
              </div>
              <div class=""col-6"">
                  <div class=""item mb-2"">
                      @{
                          var vat = await info.Item.Company.GetVat();
                      }
                      Sub Totals VAT @Html.Raw(""@"") @vat.value % on tax rate @vat.rate @info.Item.TotalVat.ToString(""c"", info.Culture)
                  </div>
              </div>
              <div class=""col-6"">
                  <div class=""item mb-2"">
                      Number of free consignment: @info.Item.NumberOfFreeConsignments
                  </div>
              </div>
              <div class=""col-6"">
                  <div class=""item mb-2"">
                      Total Discount: @info.Item.TotalDiscount.ToString(""c"", info.Culture)
                  </div>
              </div>
              <div class=""col-6"">
                  <div class=""item mb-2"">
                      Invoice must be paid in full in the billing currency
                  </div>
              </div>
              <div class=""col-6"">
                  <div class=""item mb-2"">
                      Invoice Total: @info.Item.Total.ToString(""c"", info.Culture)
                  </div>
              </div>
            <div class=""col-6"">
                <div class=""item mb-2"">
                    Bankers: @Html.Raw(Settings.Current.Bankers)
                </div>
                <div class=""item mb-2"">
                    Sort Code: @Html.Raw(Settings.Current.SortCode)
                </div>
                <div class=""item mb-2"">
                    Account Number: @Html.Raw(Settings.Current.AccountNo)
                </div>
                <div class=""item mb-2"">
                    IBAN: @Html.Raw(Settings.Current.IBAN)
                </div>
                <div class=""item mb-2"">
                    BIC: @Html.Raw(Settings.Current.BIC)
                </div>
            </div>
            <div class=""col-6"">
                <div class=""item mb-2"">
                    <b>VAT Codes</b>
                </div>
                <div class=""item mb-2"">
                    Z = Zero Rated
                </div>
                <div class=""item mb-2"">
                    S = Standard
                </div>
                <div class=""item mb-2"">
                    E = Exempt
                </div>
            </div>
          </div>
      </div>
</div>
");

            ViewModelProperty<System.Globalization.CultureInfo>("Culture");

            OnPostBound("assign")
                .Code(@"info.Culture = info.Item.Charge.CurrencyId == ChargeCurrencyOption.Euro ? System.Globalization.CultureInfo.GetCultureInfo(""en-ie"") : System.Globalization.CultureInfo.GetCultureInfo(""en-GB"");");
        }
    }
}