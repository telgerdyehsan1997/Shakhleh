using MSharp;

namespace Modules
{
    class HealthCertificateList : BaseListModule<Domain.HealthCertificate>
    {
        public HealthCertificateList()
        {
            HeaderText("Health Certificate");

            Search(GeneralSearch.AllFields).Label("Find");
            Search(x => x.IsDeactivated).Control(ControlType.HorizontalRadioButtons).Label("Status").DefaultValueExpression("false");

            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Code);
            Column(x => x.Description);

            ButtonColumn("Edit").HeaderText("Edit").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.Settings.HealthCertificatePage>().Send("item", "item.ID").SendReturnUrl());

            this.ArchiveButtonColumn("HealthCertificate");

            Button("New Health Certificate").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.Settings.HealthCertificatePage>().SendReturnUrl());
        }
    }
}