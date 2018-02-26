import { Component } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { UtilityService } from '../../services/utility.service';
import { PaymentService } from '../../services/payment.service';
import { PaymentDetails } from '../../viewmodels/paymentdetails';
import { PaymentDetailsValidator } from '../../validators/paymentdetails.validator';

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

    constructor(private utilityService: UtilityService, private paymentService: PaymentService) {
        this.form = <FormGroup>this.utilityService.generateReactiveForm(new FormGroup({}), new PaymentDetails());
        PaymentDetailsValidator.apply(this.form);
    }

    public submit() {
        this.triedToSubmitForm = true;
        if (!this.form.valid) {
            return;
        }

        this.paymentService.submit(<PaymentDetails>this.form.value)
            .subscribe(
            data => {
                this.receiptNum = data;
                this.showSuccess = true;
                this.showError = false;

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

                this.showSuccess = false;
                this.showError = true;
            });
    }

    public submitAgain() {
        this.form.reset();
        this.triedToSubmitForm = false;
        this.showSuccess = false;
    }
}
