
import OlivePage from 'olive/olivePage';
import Config from 'olive/config';
import PostCodeLookup from 'components/postcode-lookup'
import AutoComplete from 'olive/plugins/autoComplete';
import DatePickerUtil from './components/datepicker-util';
import { ServiceContainer } from 'olive/di/serviceContainer';
import Services from 'olive/di/services';
import { ServiceDescription } from 'olive/di/serviceDescription';
import { ModalHelper } from "olive/components/modal";
import { TimelineMax, Linear } from 'gsap'

export default class AppPage extends OlivePage {

    constructor() {

        super();
        // Any code you write here will run only once when the page is loaded.
    }

    configureServices(services: ServiceContainer) {
        const out: IOutParam<ServiceDescription> = {};


        services.addSingleton(Services.DatePickerFactory,
            (modalHelper: ModalHelper) =>
                new DatePickerUtil(modalHelper)
        ).withDependencies(Services.ModalHelper);

        super.configureServices(services);
    }

    initialize() {
        super.initialize();
        // This function is called upon every Ajax update as well as the initial page load.
        // Any custom initiation goes here.

        //Override the 'enableCustomCheckbox' and 'enableCustomRadio' with empty methods to use the original controls.
        this.wrapPagination();
        this.enablePostCodeLookups();

        AutoComplete.setOptions({ maxItem: 0 });
        //AutoComplete.setOptions({ searchOnFocus: false });
        this.makeAllInputUpperCase();
        this.bindBox63Non_EUTextChanged();
        this.disableBrowserAutoComplete();
        this.disableModelCloseButtonForMFA();
        this.countryNumbers();
        this.defaultcountryNumbers();
        this.countryNumberButton();
        this.removeNullRadioValues();
        this.loadPostCodeText();
        this.addCrossOnSelect();
        this.addKeepCancelCrossButton();
        this.UpdateIdRadioValues();
        this.SetRedTicketTable();
        this.SetCheckOnlyOne();

        this.RemoveSpace();

        this.ShiftAddMoreBUtton();

        this.urlEncodeSearchTextFields();

        this.FocusPatner();
        this.RemovedFocus();
        this.Banner();
        this.SetHighLighted();
    }

    disableModelCloseButtonForMFA() {
        $(".mfa")
            .parentsUntil(".modal-dialog")
            .find("button[class=close]")
            .remove();
    }

    disableBrowserAutoComplete() {
        $("input[data-control=date-picker]").attr("autocomplete", "off");
    }

    enablePostCodeLookups() {
        new PostCodeLookup($(".company-postcode-lookup"), {
            onTypeResult: false,
            initialPostcode: $("#Postcode").val(),
            inputToUpdate: $("#Postcode"),
            fields: [
                { element: $("#Postcode"), addressParts: ["postcode"] },
                { element: $("#Town"), addressParts: ["post_town"] },
                { element: $("#AddressLine2"), addressParts: ["line_2", "line_3"] },
                { element: $("#AddressLine1"), addressParts: ["line_1"] },
            ]
        });

        new PostCodeLookup($(".company-postcode-lookup"), {
            onTypeResult: false,
            initialPostcode: $("#AuthorisedPostcode").val(),
            inputToUpdate: $("#AuthorisedPostcode"),
            fields: [
                { element: $("#AuthorisedPostcode"), addressParts: ["postcode"] },
                { element: $("#AuthorisedTownOrCity"), addressParts: ["post_town"] },
                { element: $("#AuthorisedAddressLine2"), addressParts: ["line_2", "line_3"] },
                { element: $("#AuthorisedAddressLine1"), addressParts: ["line_1"] },
            ]
        });
    }



    wrapPagination() {
        var pagingList = $('.pagination');
        var pagingDropDown = $('.pagination-size');
        pagingList.addClass('pro-pagination');
        pagingDropDown.addClass('pro-pagination');
        $('.pro-pagination').wrapAll("<div class='pagination-wrapper' />");
    }
    makeAllInputUpperCase() {
        $(document).ready(function () {
            $("input[type=text]:not(#Email,.persist-casing),textarea:not(.persist-casing),input[type=text]:not(#EmailAddress,.persist-casing)").keyup(function () {
                $(this).val($(this).val().toUpperCase());
            });
            $("input[type=text]:not(#Email,.persist-casing),textarea:not(.persist-casing),input[type=text]:not(#EmailAddress,.persist-casing)").focusout(function () {
                $(this).val($(this).val().toUpperCase());
            });
        });
    }

    bindBox63Non_EUTextChanged() {
        $(document).ready(function () {
            var frm = $('form[data-module="ConsignmentForm"]');
            if (frm.length > 0) {
                var box63Non_EU = frm.find('input[name="Box63Non_EU"]');
                if (box63Non_EU.length > 0) {
                    var value = (100 - parseFloat(box63Non_EU.val())).toFixed(2);
                    var box63EU = frm.find('span[name="Box68EU"]');
                    if (value != "NaN") {
                        box63EU.text(value.toString());
                    }
                }
            }
        });
    }


    countryNumbers() {
        var count = 0;
        $('.subform-item').each(function (index, el) {
            if ($(el).css('display') == 'none')
                return;
            $(el).find('.country-label').html('Country ' + (count + 3));
            count++;
        });
    }

    defaultcountryNumbers() {
        var count = 0;
        $('.subform-item').each(function (index, el) {
            if ($(el).css('display') == 'none')
                return;
            $(el).find('.country-label-default').html('Country ' + (count + 2));
            count++;
        });
    }

    countryNumberButton() {
        let page = this;
        $(document).ready(function () {
            $('.update-country-label, [data-subform="RouteItineraryCountry"] .delete-button').click(function () {
                page.countryNumbers();
            });
        });
    }

    removeNullRadioValues() {
        $("input[type=radio]").each(function () {
            var $this = $(this);
            if ($this.val() === 'Null') {
                $this.parent().hide();
            }
        });
    }
    UpdateIdRadioValues() {
        var count = 0;
        $('[data-add-subform="HealthCertificate"]').click(function () {
            $('.form-check').each(function (index, el) {
                $(el).find('label').attr('for', 'HealthCertificate' + (count + 1));
                $(el).find('input').attr('id', 'HealthCertificate' + (count + 1));
                count++;
            });
        });
    }

    loadPostCodeText() {
        let page = this;
        $(document).on('keyup', '.postcode-text', function () {
            $('#Postcode').val($('.postcode-text').val());
            page.locationUpdate();
            $('.postcode-text').focus();
        });
    }

    locationUpdate() {
        new PostCodeLookup($(".company-postcode-lookup"), {
            onTypeResult: false,
            initialPostcode: $("#Postcode").val(),
            inputToUpdate: $("#Postcode"),
            fields: [
                { element: $("#Postcode"), addressParts: ["postcode"] },
                { element: $("#Town"), addressParts: ["post_town"] },
                { element: $("#AddressLine2"), addressParts: ["line_2", "line_3"] },
                { element: $("#AddressLine1"), addressParts: ["line_1"] },
            ]
        });
    }
    addCrossOnSelect() {
        $("select[data-show-cross]")
            .each(function (index, elem) {
                $(elem).after('<span class="boostrap_cancel-button">×</span>');
            });

        $(document).on('click', '.boostrap_cancel-button', function () {
            const select = $(this).parent().find('select')
            $(select).val('');
            $(select).selectpicker('refresh');
        });
    }
    addKeepCancelCrossButton() {
        $(".typeahead__container").on("change", function () {
            console.log(this);
            $(this).addClass('custom-cancel');
        });
        $(".typeahead__container").each(function () {
            var $this = $(this);
            var input = $this.find('input.auto-complete');
            if (input)
                if ($(input).val() != '')
                    $(this).addClass('custom-cancel');
        });
    }
    SetRedTicketTable() {
        $('[data-sort="Action.Name"]').attr("style", "display:none");
        $('.action-highlight').attr("style", "display:none");

        $('.action-highlight').map(function () {
            var $row = $(this);
            if ($row.text().contains("HighlightToSupervisor")) {
                $row.parent().attr("style", "background-color:red;color:white");
            }
        });
    }
    SetCheckOnlyOne() {
        $('[data-module = "StatusEmailNotificationsShipmentList"]').find('input[type="checkbox"]').on('change', function () {
            $(this).closest('tr').find('input').not(this).prop('checked', false);
        });
        $('[data-module = "StatusEmailNotificationsNCTSShipmentList"]').find('input[type="checkbox"]').on('change', function () {
            $(this).closest('tr').find('input').not(this).prop('checked', false);
        });
        $('[data-module = "StatusEmailNotificationsCustomerShipmentList"]').find('input[type="checkbox"]').on('change', function () {
            $(this).closest('tr').find('input').not(this).prop('checked', false);
        });
        $('[data-module="StatusEmailNotificationCustomerNCTSShipmentList"]').find('input[type="checkbox"]').on('change', function () {
            $(this).closest('tr').find('input').not(this).prop('checked', false);
        });
        $('[data-module="StatusEmailNotificationsUserCustomerShipmentList"]').find('input[type="checkbox"]').on('change', function () {
            $(this).closest('tr').find('input').not(this).prop('checked', false);
        });
        $('[data-module="StatusEmailNotificationUserCustomerNCTSShipmentList"]').find('input[type="checkbox"]').on('change', function () {
            $(this).closest('tr').find('input').not(this).prop('checked', false);
        });
    }
    RemoveSpace() {
        $("#SequenceNumber").keypress(function (e) {
            $(this).val($(this).val().replace(" ", ''));
        });
    }
    ShiftAddMoreBUtton() {
        $('form[data-module="DutyCalculatorForm"] .form-group:has(.btn-secondary)').css('float', 'right');
    }

    urlEncodeSearchTextFields() {
        $('button[type="submit"][must-url-encode="true"]').click(function () {
            let textBoxes = $('.search').find('input[type="text"]');
            textBoxes.each(function () {
                $(this).val(encodeURIComponent($(this).val()));
            });
        });

    }
    FocusPatner() {
        if ($("#UKTrader_Text").val() !== "") {
            $("#UKTrader_Text").removeAttr("autofocus");

            if ($("#Partner_Text").val() === "") {
                $("#Partner_Text").focus();
            }
        }
        if ($("#Partner_Text").val() !== "") {
            $("#Partner_Text").removeAttr("autofocus");
        }
        if ($("#Declarant_Text").val() !== "") {
            $("#Declarant_Text").removeAttr("autofocus");
        }
    }
    RemovedFocus() {
        $(".float-left").on("click", function () {
            if ($("#UKTrader_Text").val() !== "") {
                $("#UKTrader_Text").removeAttr("autofocus");
            }
            if ($("#Partner_Text").val() !== "") {
                $("#Partner_Text").removeAttr("autofocus");
            }
            if ($("#Declarant_Text").val() !== "") {
                $("#Declarant_Text").removeAttr("autofocus");
            }
        })
    }
    Banner() {
        var $tickerWrapper = $(".tickerwrapper");
        var $list = $tickerWrapper.find("ul.list");
        var $clonedList = $list.clone();
        var listWidth = $tickerWrapper.width();

        $list.find("li").each(function (i) {
            listWidth += $(this, i).outerWidth(true);
        });

        //var endPos = $tickerWrapper.width() - listWidth;

        $list.add($clonedList).css({
            "width": listWidth + "px"
        });

        $clonedList.addClass("cloned").appendTo($tickerWrapper);

        //TimelineMax
        var infinite = new TimelineMax({ repeat: -1, paused: true });
        var time = 20;

        infinite
            .fromTo($list, time, { rotation: 0.01, x: 0 }, { force3D: true, x: -listWidth, ease: Linear.easeNone }, 0)
            .fromTo($clonedList, time, { rotation: 0.01, x: listWidth }, { force3D: true, x: 0, ease: Linear.easeNone }, 0)
            .set($list, { force3D: true, rotation: 0.01, x: listWidth })
            .to($clonedList, time, { force3D: true, rotation: 0.01, x: -listWidth, ease: Linear.easeNone }, time)
            .to($list, time, { force3D: true, rotation: 0.01, x: 0, ease: Linear.easeNone }, time)
            .progress(1).progress(0)
            .play();

        //Pause/Play		
        $tickerWrapper.on("mouseenter", function () {
            infinite.pause();
        }).on("mouseleave", function () {
            infinite.play();
        });
    }
    SetHighLighted() {
        $(".action-highlighted").parent().parent().css({ "background-color": "red" });
    }
}



window["page"] = new AppPage();