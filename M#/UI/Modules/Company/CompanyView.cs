using MSharp;

namespace Modules
{
    class CompanyView : ViewModule<Domain.Company>
    {
        public CompanyView()
        {
            DataSource("info.Company");

            HeaderText("@info.Item.Name");

            CustomField().LabelText("Transaction type(s)").DisplayExpression(@"@((await info.Item.TransactionTypes.GetList()).Select(t => t.ShipmentType).ToString("", ""))");
            Field(x => x.Type);
            Field(x => x.CustomerAccountNumber);
            Field(x => x.RefrenceSuffix).LabelText("Reference Suffix");
            Field(x => x.Country);
            Field(x => x.Postcode).LabelText("Postcode/Zip code");
            Field(x => x.AddressLine1);
            Field(x => x.AddressLine2);
            Field(x => x.Town);
            Field(x => x.EORINumber);
            Field(x => x.BranchIdentifier);
            Field(x => x.AEONumber);
            //Field(x => x.TSP);
            Field(x => x.CFSPType).LabelText("CFSP");
            //Field(x => x.CFSP).DisplayExpression("@(item.CFSP == true ? \"Yes\" : \"No\" )"); ;
            Field(x => x.DefaultDeclarant);
            //Field(x => x.PaymentCode);
            Field(x => x.PaymentType);
            Field(x => x.DefermentNumber);
            Field(x => x.RepresentationType).DisplayExpression("@(info.Item.RepresentationType ? \"Direct\" : \"Indirect\" )");
            Field(x => x.GuaranteeNumber).LabelText("Transit Guarantee");
            Field(x => x.GuaranteeType);
            Field(x => x.TIN);
            Field(x => x.PIN);
            Field(x => x.AuthorisedLocationsLinks).LabelText("Authorised location")
                .DisplayExpression(@"@(await item.AuthorisedLocationsLinks.GetList().Select(x=>x.Authorisedlocation.LocationName).ToString("" | ""))");

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
        }
    }
}