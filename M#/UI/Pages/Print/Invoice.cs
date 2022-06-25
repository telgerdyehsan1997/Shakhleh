using MSharp;
using Domain;

public class InvoicePage : SubPage<PrintPage>
{
    public InvoicePage()
    {
        Layout(Layouts.Print);
        //add print module
        Add<Modules.InvoicePrintHeader>();
        Add<Modules.InvoiceChargePrintList>();
        Add<Modules.InvoiceTransactionPrintList>();
        Add<Modules.InvoicePrintFooter>();
    }
}
