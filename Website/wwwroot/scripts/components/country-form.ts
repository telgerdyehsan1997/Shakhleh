import 'app/appPage';

export default class CountryForm {

    public static run(): void {

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

        var isNew = (window.location.href.split("/")[7])?.split("?")[0];
        console.log(isNew);
        if (isNew === null || isNew === "" || typeof (isNew) === "undefined") {
            $('input[name ="EU27"]').prop('checked', false);
            $('input[name ="HasMFN"]').prop('checked', false);
            $('input[name ="PreferenceAvailable"]').prop('checked', false);
            $("#MFNCode1").parent().parent().hide()
            $("#MFNCode2").parent().parent().hide()
            $("#MFNCode3").parent().parent().hide()
            $("#MFNCode4").parent().parent().hide()
            $("#MFNCode5").parent().parent().hide()

            $("#ImportCPCWithPreference").parent().parent().hide()
            $("#ImportCPCWithPreferenceDeclarationType").parent().parent().hide()
            $("#ImportCPCWithPreferencePreferenceCode").parent().parent().hide()
            $("#ImportCPCWithPreferenceRateCode").parent().parent().hide()
            $("#ExportCPCWithPreference").parent().parent().hide()
            $("#ExportCPCWithPreferenceDeclarationType").parent().parent().hide()
        } else {
            $("#MFNCode1").show()
            $("#MFNCode2").show()
            $("#MFNCode3").show()
            $("#MFNCode4").show()
            $("#MFNCode5").show()
            $("#MFNCode1").parent().parent().show()
            $("#MFNCode2").parent().parent().show()
            $("#MFNCode3").parent().parent().show()
            $("#MFNCode4").parent().parent().show()
            $("#MFNCode5").parent().parent().show()

            $("#ImportCPCWithPreference").show()
            $("#ImportCPCWithPreferenceDeclarationType").show()
            $("#ImportCPCWithPreferencePreferenceCode").show()
            $("#ImportCPCWithPreferenceRateCode").show()
            $("#ExportCPCWithPreference").show()
            $("#ExportCPCWithPreferenceDeclarationType").show()

            $("#ImportCPCWithPreference").parent().parent().show()
            $("#ImportCPCWithPreferenceDeclarationType").parent().parent().show()
            $("#ImportCPCWithPreferencePreferenceCode").parent().parent().show()
            $("#ImportCPCWithPreferenceRateCode").parent().parent().show()
            $("#ExportCPCWithPreference").parent().parent().show()
            $("#ExportCPCWithPreferenceDeclarationType").parent().parent().show()
        }

        $('input[name ="HasMFN"]').change(function () {
            var hasvalue = $('input[name ="HasMFN"]:checked').val();
            if (hasvalue === 'True') {
                //show the fileds
                $("#MFNCode1").show()
                $("#MFNCode2").show()
                $("#MFNCode3").show()
                $("#MFNCode4").show()
                $("#MFNCode5").show()
                $("#MFNCode1").parent().parent().show()
                $("#MFNCode2").parent().parent().show()
                $("#MFNCode3").parent().parent().show()
                $("#MFNCode4").parent().parent().show()
                $("#MFNCode5").parent().parent().show()
            } else {
                //hide all the fileds
                $("#MFNCode1").hide()
                $("#MFNCode2").hide()
                $("#MFNCode3").hide()
                $("#MFNCode4").hide()
                $("#MFNCode5").hide()
                $("#MFNCode1").parent().parent().hide()
                $("#MFNCode2").parent().parent().hide()
                $("#MFNCode3").parent().parent().hide()
                $("#MFNCode4").parent().parent().hide()
                $("#MFNCode5").parent().parent().hide()
            }
        });
        $('input[name ="PreferenceAvailable"]').change(function () {
            var hasvalue = $('input[name ="PreferenceAvailable"]:checked').val();
            if (hasvalue === 'True') {
                //show the fileds
                $("#ImportCPCWithPreference").show()
                $("#ImportCPCWithPreferenceDeclarationType").show()
                $("#ImportCPCWithPreferencePreferenceCode").show()
                $("#ImportCPCWithPreferenceRateCode").show()
                $("#ExportCPCWithPreference").show()
                $("#ExportCPCWithPreferenceDeclarationType").show()

                $("#ImportCPCWithPreference").parent().parent().show()
                $("#ImportCPCWithPreferenceDeclarationType").parent().parent().show()
                $("#ImportCPCWithPreferencePreferenceCode").parent().parent().show()
                $("#ImportCPCWithPreferenceRateCode").parent().parent().show()
                $("#ExportCPCWithPreference").parent().parent().show()
                $("#ExportCPCWithPreferenceDeclarationType").parent().parent().show()
            } else {
                //hide all the fileds
                $("#ImportCPCWithPreference").hide()
                $("#ImportCPCWithPreferenceDeclarationType").hide()
                $("#ImportCPCWithPreferencePreferenceCode").hide()
                $("#ImportCPCWithPreferenceRateCode").hide()
                $("#ExportCPCWithPreference").hide()
                $("#ExportCPCWithPreferenceDeclarationType").hide()
                $("#ImportCPCWithPreference").parent().parent().hide()
                $("#ImportCPCWithPreferenceDeclarationType").parent().parent().hide()
                $("#ImportCPCWithPreferencePreferenceCode").parent().parent().hide()
                $("#ImportCPCWithPreferenceRateCode").parent().parent().hide()
                $("#ExportCPCWithPreference").parent().parent().hide()
                $("#ExportCPCWithPreferenceDeclarationType").parent().parent().hide()
            }
        });
    }
}
