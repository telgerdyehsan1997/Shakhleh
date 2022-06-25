import CompanyAutoset from "./company-autoset.js";

export default class AddConsignmentForm {
    public static run(): void {
        $('button[name=AddCompany]').click((event) => {
            CompanyAutoset.Selector = $(event.currentTarget).closest(".form-group").find(":text").getUniqueSelector();
        });

        $(".from-package").change(function () {
            $(".to-package").val($(this).val())
        });

        $(".from-grossweight").change(function () {
            $(".to-grossweight").val($(this).val())
        });

        $(".from-netweight").change(function () {
            $(".to-netweight").val($(this).val())
        });
    }
}