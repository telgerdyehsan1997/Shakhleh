import 'app/appPage';
import PostCodeLookup from 'components/postcode-lookup'

export default class CompanyForm {

    public static run(): void {
        // Check if EORI number is mandatory (Country field set to UK/GB)
        var field = $('#EORINumber');
        var parentContainer = field.closest("div.form-group.row");
        if (parentContainer.hasClass("required-item")) {
            field.attr("data-rule-required", "true");
            field.attr("data-msg-required", "The EORI number field is required.");
        }

        // Autofocus on Company name field
        $('#Name').focus();


        //show hide DefermentNumber based on payment type
        var paymentvalue = $("#PaymentType");
        paymentvalue.change(function () {
            if (paymentvalue.val()) {
                paymentvalue.parent().closest('.form-group').next().removeClass("hidden")
            } else {
                paymentvalue.parent().closest('.form-group').next().addClass("hidden");
            }
        });
        $(document).ready(function () {
            if ($('#Postcode').val() !== null) {
                $('.postcode-text').val($('#Postcode').val());
            }
        });

    }
}

