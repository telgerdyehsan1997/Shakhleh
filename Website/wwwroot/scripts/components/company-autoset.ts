import CrossDomainEvent from "olive/components/crossDomainEvent";

export default class CompanyAutoset {
    static Selector: string;
    static Text: string;

    public static run(): void {
        CrossDomainEvent.handle("close-modal", (arg) => {
            if (CompanyAutoset.Text && CompanyAutoset.Selector) {
                $(CompanyAutoset.Selector).val(CompanyAutoset.Text);
                $(CompanyAutoset.Selector).focus();
            }

            CompanyAutoset.Selector = null;
            CompanyAutoset.Text = null;
        });

        $('button[name=Save]').click((event) => {
            CompanyAutoset.Text = `${$("#Name").val()} - ${$("#Town").val()} - ${$("#Postcode").val()}`;

            if ($("#EORINumber").val()) {
                CompanyAutoset.Text = CompanyAutoset.Text + ` - ${$("#EORINumber").val()}`
            }

            if ($("#DefermentNumber").val()) {
                CompanyAutoset.Text = CompanyAutoset.Text + ` - ${$("#DefermentNumber").val()}`
            }
        });
    }
}