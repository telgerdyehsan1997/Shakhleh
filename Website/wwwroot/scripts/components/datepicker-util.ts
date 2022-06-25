import Config from "olive/config"
import { DatePickerFactory } from "olive/plugins/datePicker";
import dateTimePickerBase from "olive/plugins/dateTimePickerBase";
import { ModalHelper } from "olive/components/modal";

export default class DatePickerUtil extends DatePickerFactory {
    constructor(private modalHelper1: ModalHelper) {
        super(modalHelper1);
    }

    public enable(selector: JQuery) { selector.each((i, e) => new DatePickerBaseUtil($(e), this.modalHelper1).show()); }
}

export class DatePickerBaseUtil extends dateTimePickerBase {
    protected controlType = "date-picker";
    protected format = Config.DATE_FORMAT;

    constructor(targetInput: JQuery, modalHelper: ModalHelper) {
        super(targetInput, modalHelper);
    }

    protected modifyOptions(options: any): void {
        let date = new Date();
        const minDateAttr = this.input.attr("data-min-past");
        console.log(minDateAttr);
        if (minDateAttr)
            date = new Date(minDateAttr);
        date.setHours(0, 0, 0, 0);

        $.extend(options, {
            viewMode: this.input.attr("data-view-mode") || 'days',
            minDate: this.input.attr("data-disable-past") != undefined ? date : undefined
        });
    }
}