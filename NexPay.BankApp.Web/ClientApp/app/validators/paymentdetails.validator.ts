import { FormGroup, FormControl, AbstractControl, ValidatorFn, Validators } from '@angular/forms';


export class PaymentDetailsValidator {
    static apply(group: FormGroup): any {
        let prop = group.get("bsb");
        if (prop) {
            prop.setValidators([
                Validators.required,
                Validators.pattern("^[1-9]{1}[0-9]{5}$")
            ]);
        }

        prop = group.get("accountNumber");
        if (prop) {
            prop.setValidators([
                Validators.required,
                Validators.pattern("^[1-9]{1}[0-9]{7}$")
            ]);
        }

        prop = group.get("accountName");
        if (prop) {
            prop.setValidators([
                Validators.required,
                Validators.pattern("^[a-zA-Z0-9]{1,32}$")
            ]);
        }

        prop = group.get("amount");
        if (prop) {
            prop.setValidators([
                Validators.required,
                Validators.pattern("^[0-9]+(\.[0-9]{1,2})?$")
            ]);
        }
    }
}