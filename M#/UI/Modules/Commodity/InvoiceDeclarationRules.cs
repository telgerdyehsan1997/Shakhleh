using MSharp;

namespace Modules
{
    class InvoiceDeclarationRulesView : FormModule<Domain.Consignment>
    {
        public InvoiceDeclarationRulesView()
        {
            HeaderText("Invoice Declaration Rules");
            Header(@"<p>For goods from the EU with a value of less than €6000 to claim preference the invoice / commercial documents should contain the following statement</p>
            <p>
                <b> 
               <p>The exporter of the products covered by this document declares that, except where otherwise clearly indicated, these products are of (name of country) preferential origin.</p>
                <p>…………………………………………………………… </p>
                (Place and date)</p>
                <p>……………………………………………………………</p>
                <p>(Name of the exporter)</p>
                </b>
            </p>
            <p>
             <br>
                For goods from the EU with a value greater then €6000 to claim preference the invoice / commercial documents should contain the following statement.
             <br>
            </p>
            <p>
              <b>  <p>The exporter of the products covered by this document(*Exporter Reference No …) declares that, except where otherwise clearly indicated, these products are of … (name of country(s) preferential origin.</p>
                <p>…………………………………………………………… </p>
                <p>(Place and date)</p>
               <p> ……………………………………………………………</p>
                <p>(Name of the exporter)</p>
                <p>*Exporter Reference No is the exporters REX approval number.</p>
               <p> This <a href=""https://www.gov.uk/guidance/import-and-export-goods-using-preference-agreements"" target=""_blank"">link</a> will take you to HMRC website offering additional guidance on if you are entitled to claim a reduced rate of duty.</p>
            </b>
           </p>");
            Button("Ok").OnClick(x => x.CloseModal());
        }
    }
}