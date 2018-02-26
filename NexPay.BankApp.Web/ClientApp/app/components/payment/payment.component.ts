import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { UtilityService } from '../../services/utility.service';
import { PaymentService } from '../../services/payment.service';
import { LoggerService } from '../../services/logger.service';
import { PaymentDetails } from '../../viewmodels/paymentdetails';
import { PaymentDetailsValidator } from '../../validators/paymentdetails.validator';
import { Severity } from '../../enums/severity';

@Component({
    selector: 'payment',
    templateUrl: './payment.component.html'
})
export class PaymentComponent {
    public form: FormGroup;
    public showSuccess: boolean = false;
    public showError: boolean = false;
    private errorMessages: any = [];
    public receiptNum: string;
    public triedToSubmitForm: boolean = false;
    private object: Object = Object;

    constructor(private utilityService: UtilityService, private paymentService: PaymentService,
        private loggerService: LoggerService) {
        this.form = <FormGroup>this.utilityService.generateReactiveForm(new FormGroup({}), new PaymentDetails());
        PaymentDetailsValidator.apply(this.form);
    }

    public submit() {
        this.triedToSubmitForm = true;
        if (!this.form.valid) {
            return;
        }

        this.loggerService.Log("Trying to submit payment", Severity.Info);

        this.paymentService.submit(<PaymentDetails>this.form.value)
            .subscribe(
            data => {
                //Make all model properties as pristine
                //Object.keys(this.form.value).forEach(value => this.form.controls[value].markAsPristine());
                this.receiptNum = data;
                this.showSuccess = true;
                this.showError = false;

                this.loggerService.Log("Payment " + this.receiptNum + " has successfully submitted", Severity.Info);
            }, (err) => {
                if (!err.ok && err._body) {
                    var responseMessage = JSON.parse(err._body);
                    if (responseMessage.errors) {
                        this.errorMessages = responseMessage.errors;
                    } else {
                        this.errorMessages = [err.statusText];
                    }
                } else {
                    this.errorMessages = [err.statusText];
                }

                this.loggerService.Log("Error ocurred during the payment processing: Errors: " + this.errorMessages, Severity.Error);

                this.showSuccess = false;
                this.showError = true;
            });
    }

    public submitAgain() {
        this.loggerService.Log("The user selected to submit a new payment", Severity.Info);

        this.form.reset();
        this.triedToSubmitForm = false;
        this.showSuccess = false;
    }
}