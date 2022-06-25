import $ = require('jquery');
import 'jquery-ui/ui/widget';
import 'bootstrap';
import 'jquery-validate-unobtrusive';
import 'jquery-validate';
import 'validation-style';
import 'olive/olivePage';
import 'app/appPage';

interface PostCodeResult {
    building_number: string;
    building_name: string;
    line_1: string;
    line_2: string;
    line_3: string;
    post_town: string;
    county: string;
    postcode: string;
}

export default class PostCodeLookup {
    addresses: PostCodeResult[] = [];
    $addressesList: JQuery;
    $searchTextBox: JQuery;
    $searchButton: JQuery;
    highlightedAddressIndex: number = -1;
    lastSearchPostCode: string;
    timerHandler: any;

    constructor(private $baseElement: JQuery, private config: any) {
        this.initialize();
        this.render();
    }

    initialize = () => {
        if (!this.config.hasOwnProperty("showFindButton"))
            this.config["showFindButton"] = true;
        if (!this.config.hasOwnProperty("onTypeResult"))
            this.config["onTypeResult"] = true;

        if (this.config["onTypeResult"] == true) {
            this.$baseElement.on("change keyup click focusin", ".postcode-text", (event) => {
                if (event.keyCode == 37 || event.keyCode == 38 || event.keyCode == 39 || event.keyCode == 40 || event.keyCode == 13 || event.keyCode == 9)
                    return;
                this.clearTimer();
                this.timerHandler = setTimeout(() => {
                    this.findAddresses();
                }, 400);
            });
        }
        $(document).on("click.postcodelookup", (event) => {
            if ($(event.target).parents(".postcode-search").length == 0) {
                this.hideAddressesList();
            }
        });
    }

    render = () => {
        this.$baseElement.empty().addClass("postcode-lookup");
        this.$searchTextBox = $("<input type='text' autocomplete='new-address-text'>").addClass("form-control")
            .addClass("postcode-text").attr("placeholder", "Postcode");
        if (this.config.hasOwnProperty("initialPostcode"))
            this.$searchTextBox.val(this.config.initialPostcode);
        this.$searchButton = $("<input type='button'>").addClass("btn btn-secondary").val("Find Address");
        this.$addressesList = $("<div>").addClass("postcode-addresses").css("display", "none");
        this.$baseElement.append($("<div>").addClass("postcode-search").append(this.$searchTextBox, this.$addressesList))
        if (this.config.hasOwnProperty("showFindButton") && this.config["showFindButton"] == true) {
            this.$baseElement.append(this.$searchButton);
            this.$searchButton.on("click", () => {
                event.preventDefault();
                this.findAddresses();
                this.$searchTextBox.focus();
                return false;
            });
        }

        this.$searchTextBox.on("keyup", (event) => {
            if (this.config.hasOwnProperty("inputToUpdate"))
                (<JQuery>this.config.inputToUpdate).val(this.$searchTextBox.val());
        });
        this.$searchTextBox.on("keydown", (event) => {
            let index = this.highlightedAddressIndex;
            if (event.keyCode == 38) { // up
                event.preventDefault();
                index--;
            } else if (event.keyCode == 40) { // down
                event.preventDefault();
                index++;
                if (index >= this.addresses.length)
                    index = this.addresses.length - 1;
            } else if (event.keyCode == 9) { //tab
                this.hideAddressesList();
                return;
            } else if (event.keyCode == 13 && this.config["onTypeResult"] == false) { // enter
                event.preventDefault();
                if (this.config["showFindButton"] == true && (this.$addressesList.css("display") == "none" || this.addresses.length == 0)) {
                    this.findAddresses();
                    return;
                }
                if (index > -1 && index < this.addresses.length) {
                    this.fillAddress(this.addresses[index]);
                    this.$searchTextBox.focusout(() => { });
                    (<JQuery>this.config["fields"][this.config["fields"].length - 1].element).focus();
                    return;
                }
            }

            if (index < 0)
                index = 0;

            this.highlightAddress(index);
        });

        if (this.config.hasOwnProperty("postcode") && this.config["postcode"] != '')
            this.$searchTextBox.val(this.config["postcode"]);
    }

    renderAddresses = (errorMessage: string) => {
        this.$addressesList.empty();

        if (this.addresses.length == 0)
            this.$addressesList.append($("<div>").addClass("postcode-address-empty").html(errorMessage || "No address found."));

        this.addresses.forEach((address, index) => {
            let $addressItem = $("<div>").addClass("postcode-address-item").attr("data-index", index).text(this.getAddressText(address)).val(index.toString());
            this.$addressesList.append($addressItem);
        });

        this.$addressesList.find(".postcode-address-item").on("click", (event) => {
            let addressIndex = parseInt($(event.target).attr("data-index"));
            this.fillAddress(this.addresses[addressIndex]);
            this.$searchTextBox.focusout(() => { });
            (<JQuery>this.config["fields"][this.config["fields"].length - 1].element).focus();
            this.hideAddressesList();
        });

        this.$addressesList.find(".postcode-address-item").on("mouseover", (event) => {
            this.highlightAddress(parseInt($(event.target).attr("data-index")));
        });

        this.$addressesList.css("display", "");
    };

    highlightAddress = (index: number) => {
        this.$addressesList.find(".postcode-address-item").removeClass("highlighted-address");
        this.$addressesList.find(".postcode-address-item[data-index=" + index.toString() + "]").addClass("highlighted-address");
        this.highlightedAddressIndex = index;
    }

    findAddresses = () => {
        if (this.$searchTextBox.val() == null || this.$searchTextBox.val().trim() == "")
            return;
        if (this.lastSearchPostCode == this.$searchTextBox.val()) {
            this.$addressesList.css("display", "");
            return;
        }
        this.lastSearchPostCode = this.$searchTextBox.val();
        let $this = this;
        $.post(this.config["api"] || "/api/postcodelookup", { postcode: this.lastSearchPostCode }, result => {
            let response = JSON.parse(result);
            this.addresses = response.result;
            this.highlightedAddressIndex = -1;
            this.renderAddresses(response["error"]);
        }).fail(function (xhr, status, msg) {
            $this.addresses = [];
            $this.renderAddresses("No address found");
        });
    }

    fillAddress = (address: PostCodeResult) => {
        (<any[]>this.config["fields"]).forEach(field => {
            let parts = (<string[]>field["addressParts"]).map(part => address[part]);
            (<JQuery>field["element"]).val(this.joinString(parts));
        });
        this.$addressesList.css("display", "none");
        this.$searchTextBox.val(address.postcode);
    }

    hideAddressesList = () => {
        this.clearTimer();
        this.$addressesList.css("display", "none");
    }

    getAddressText = (address: PostCodeResult): string => {
        return this.joinString([
            //address.building_number,
            //address.building_name,
            address.line_1,
            address.line_2,
            address.line_3,
            address.post_town,
            address.county,
            address.postcode,
        ]);
    }

    clearTimer = () => {
        if (this.timerHandler != null)
            clearTimeout(this.timerHandler);
    }

    joinString = (strArray: string[]): string => {
        return strArray.filter(v => v !== '').join(', ');
    }
}