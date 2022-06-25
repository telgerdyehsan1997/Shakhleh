using MSharp;

namespace Modules
{
    class CountryList : BaseListModule<Domain.Country>
    {
        public CountryList()
        {
            HeaderText("Countries");

            RootCssClass("scrollable-list");

            this.ArchiveSearch();
            Search(GeneralSearch.AllFields)
                .MemoryFilterCode(@"if ( info.FullSearch != null) 
                                    { 
                                     if (info.FullSearch.Length == 2)
                                        result = result.Where(t => t.Code.Contains(info.FullSearch));
                                     else
                                        result = result.Where(t => t.ToString(""F"").Contains(info.FullSearch)); 
                                   }")
                .Label("Find");

            SearchButton("Search").Icon(FA.Search)
                .IsDefault()
                .CssClass("float-right")
                .OnClick(x => x.ReturnView());

            LinkColumn(x => x.Name).HeaderText("Country")
                .OnClick(x => x.Go<Admin.Settings.Countries.EnterPage>().Send("item", "item.ID").SendReturnUrl());
            Column(x => x.Code).LabelText("Country code");
            Column(x => x.EU27);
            Column(x => x.MFNCode1);
            Column(x => x.MFNCode2);
            Column(x => x.MFNCode3);
            Column(x => x.MFNCode4);
            Column(x => x.MFNCode5);
            Column(x => x.PreferenceAvailable);
            Column(x => x.ImportCPCWithPreference);
            Column(x => x.ImportCPCWithPreferenceDeclarationType);
            Column(x => x.ImportCPCWithPreferencePreferenceCode);
            Column(x => x.ImportCPCWithoutPreference);
            Column(x => x.ImportCPCWithoutPreferenceDeclarationType);
            Column(x => x.ImportCPCWithoutPreferencePreferenceCode);
            Column(x => x.ExportCPCWithPreference);
            Column(x => x.ExportCPCWithPreferenceDeclarationType);
            Column(x => x.ExportCPCWithoutPreference);
            Column(x => x.ExportCPCWithoutPreferenceDeclarationType);
            Column(x => x.InvoiceDeclarationDocumentType);
            Column(x => x.InvoiceDeclarationDocumentTypeDocumentStatus);
            Column(x => x.PreferenceCertificateNumberDocumentType);
            Column(x => x.PreferenceCertificateNumberDocumentTypeDocumentStatus);
            Column(x => x.CountryDiallingCode);

            this.ArchiveButtonColumn("Country");

            Button("New Country").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.Countries.EnterPage>().SendReturnUrl());
        }
    }
}