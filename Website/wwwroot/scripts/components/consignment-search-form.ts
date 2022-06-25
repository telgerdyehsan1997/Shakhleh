import 'app/appPage';

export default class ConsignmentSearchForm {
    public static run(): void {
        var totalGrossWeightMin = $("#TotalGrossWeightMin");
        var totalGrossWeightMax = $("#TotalGrossWeightMax")
        var totalValueMin = $("#TotalValueMin");
        var totalValueMax = $("#TotalValueMax");
        var totalPackagesMin = $("#TotalPackagesMin");
        var totalPackagesMax = $("#TotalPackagesMax")
        var totalNetWeightMin = $("#TotalNetWeightMin");
        var totalNetWeightMax = $("#TotalNetWeightMax")

        var date = $("#Date");
        var dateMax = $("#DateMax");
        var expectedDate = $("#ExpectedDate");
        var expectedDateMax = $("#ExpectedDateMax");

        var expectedDateDep = $("#ExpectedDateOfDeparture");
        var expectedDateMaxDep = $("#ExpectedDateOfDepartureMax");

        var dateCons = $("#DateCons");
        var dateMaxCons = $("#DateMaxCons");
        var expectedDateCons = $("#ExpectedDateCons");
        var expectedDateMaxCons = $("#ExpectedDateMaxCons");



        totalGrossWeightMin.change(function () {
            if (parseInt(totalGrossWeightMax.val()) < parseInt(totalGrossWeightMin.val())) {
                alert("Max value must be greater then min value");
                totalGrossWeightMin.val("");
            }
        });

        totalGrossWeightMax.change(function () {
            if (parseInt(totalGrossWeightMin.val()) > parseInt(totalGrossWeightMax.val())) {
                alert("Max value must be greater then min value");
                totalGrossWeightMax.val("");

            }
        });
        totalValueMin.change(function () {
            if (parseInt(totalValueMax.val()) < parseInt(totalValueMin.val())) {
                alert("Max value must be greater then min value");
                totalValueMin.val("");
            }
        });

        totalValueMax.change(function () {
            if (parseInt(totalValueMax.val()) < parseInt(totalValueMin.val())) {
                alert("Max value must be greater then min value");
                totalValueMax.val("");

            }
        });
        totalPackagesMin.change(function () {
            if (parseInt(totalPackagesMax.val()) < parseInt(totalPackagesMin.val())) {
                alert("Max value must be greater then min value");
                totalPackagesMin.val("");
            }
        });

        totalPackagesMax.change(function () {
            if (parseInt(totalPackagesMin.val()) > parseInt(totalPackagesMax.val())) {
                alert("Max value must be greater then min value");
                totalPackagesMax.val("");

            }
        });
        totalNetWeightMin.change(function () {
            if (parseInt(totalNetWeightMax.val()) < parseInt(totalNetWeightMin.val())) {
                alert("Max value must be greater then min value");
                totalNetWeightMin.val("");
            }
        });

        totalNetWeightMax.change(function () {
            if (parseInt(totalNetWeightMin.val()) > parseInt(totalNetWeightMax.val())) {
                alert("Max value must be greater then min value");
                totalNetWeightMax.val("");

            }
        });

        var consignmentNumber = $("#ConsignmentNumber");
        var uCR = $("#UCR");
        var lRN = $("#LRN");
        var eADMRN = $("#EADMRN");

        consignmentNumber.change(function () {
            if (consignmentNumber.val() === null || consignmentNumber.val() === "" || typeof (consignmentNumber.val()) === "undefined") {
                DateSetValue();
            } else {
                DateSet();
            }
        });
        uCR.change(function () {
            if (uCR.val() === null || uCR.val() === "" || typeof (uCR.val()) === "undefined") {
                DateSetValue();
            } else {
                DateSet();
            }
        });
        lRN.change(function () {
            if (lRN.val() === null || lRN.val() === "" || typeof (lRN.val()) === "undefined") {
                DateSetValue();
            } else {
                DateSet();
            }
        });
        eADMRN.change(function () {
            if (eADMRN.val() === null || eADMRN.val() === "" || typeof (eADMRN.val()) === "undefined") {
                DateSetValue();
            } else {
                DateSet();
            }
        });
        if (consignmentNumber.val() === null || consignmentNumber.val() === "" || typeof (consignmentNumber.val()) === "undefined") {
            DateSetValue();
        }
        if (uCR.val() === null || uCR.val() === "" || typeof (uCR.val()) === "undefined") {
            DateSetValue();
        }
        if (lRN.val() === null || lRN.val() === "" || typeof (lRN.val()) === "undefined") {
            DateSetValue();
        }
        if (eADMRN.val() === null || eADMRN.val() === "" || typeof (eADMRN.val()) === "undefined") {
            DateSetValue();
        }
        function DateSetValue() {
            dateCons.val(date.val());
            dateMaxCons.val(dateMax.val());
            expectedDateCons.val(expectedDate.val());
            expectedDateMaxCons.val(expectedDateMax.val());

            if (typeof (expectedDateDep.val()) !== "undefined") {
                expectedDateCons.val(expectedDateDep.val());
            }
            if (typeof (expectedDateMaxDep.val()) !== "undefined") {
                expectedDateMaxCons.val(expectedDateMaxDep.val());
            }
            if (typeof (expectedDate.val() !== null || expectedDate.val() !== "" || typeof (expectedDate.val()) !== "undefined") || typeof (expectedDateMax.val() !== null || expectedDateMax.val() !== "" || typeof (expectedDateMax.val()) !== "undefined")) {
                dateCons.val("");
                dateMaxCons.val("");
            }
            if (typeof (expectedDateDep.val() !== null || expectedDateDep.val() !== "" || typeof (expectedDateDep.val()) !== "undefined") || typeof (expectedDateMaxDep.val() !== null || expectedDateMaxDep.val() !== "" || typeof (expectedDateMaxDep.val()) !== "undefined")) {
                dateCons.val("");
                dateMaxCons.val("");
            }
        }
        function DateSet() {
            dateCons.val("");
            dateMaxCons.val("");
            expectedDateCons.val("");
            expectedDateMaxCons.val("");
        }

        dateCons.parent().parent().parent().hide();
        dateMaxCons.parent().parent().parent().hide();
        expectedDateCons.parent().parent().parent().hide();
        expectedDateMaxCons.parent().parent().parent().hide();

    }
}