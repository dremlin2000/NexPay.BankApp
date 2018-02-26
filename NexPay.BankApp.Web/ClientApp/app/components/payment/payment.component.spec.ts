/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms'
import { HttpModule } from '@angular/http';
import { Observable } from 'rxjs';

import { PaymentComponent } from './payment.component';
import { PaymentService } from '../../services/payment.service';
import { LoggerService } from '../../services/logger.service';
import { PaymentDetails } from '../../viewmodels/paymentdetails';
import { UtilityService } from '../../services/utility.service';

let fixture: ComponentFixture<PaymentComponent>;
let component: PaymentComponent;

describe('Payment component', () => {
    let paymentService: PaymentService;
    let receiptNum: string;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ReactiveFormsModule],
            declarations: [PaymentComponent],
            providers: [PaymentService, LoggerService, UtilityService, { provide: 'BASE_URL', useFactory: () => "TestBaseUrl" }]
        });
        fixture = TestBed.createComponent(PaymentComponent);
        component = fixture.componentInstance;
        paymentService = fixture.debugElement.injector.get(PaymentService);
        let loggerService = fixture.debugElement.injector.get(LoggerService);
        let utilityService = fixture.debugElement.injector.get(UtilityService);

        // Setup spy on the methods
        receiptNum = utilityService.newGuid();
        spyOn(loggerService, 'Log');
    });

    it('When the component is initialized and ready to use Then the form should be valid', async(() => {
        expect(component.form.valid).toBeTruthy();
    }));

    it('When I submit the empty form Then form is invalid', async(() => {
        spyOn(paymentService, 'submit').and.returnValue(Observable.of(receiptNum));
        let vm = new PaymentDetails()
        component.form.setValue(vm);

        component.submit();

        expect(component.triedToSubmitForm).toBeTruthy();
        expect(component.form.valid).toBeFalsy();
    }));

    it('When submit the valid form Then form is valid and payment service submit function is called', async(() => {
        spyOn(paymentService, 'submit').and.returnValue(Observable.of(receiptNum));
        let vm = new PaymentDetails();
        vm.bsb = 123456;
        vm.accountNumber = 12345678;
        vm.accountName = "testaccountname";
        vm.amount = 100;
        component.form.setValue(vm);

        component.submit();

        expect(component.form.valid).toBeTruthy();
        expect(component.triedToSubmitForm).toBeTruthy();
        expect(component.showSuccess).toBeTruthy();
        expect(component.showError).toBeFalsy();
        expect(component.receiptNum).toBe(receiptNum);
        expect(paymentService.submit).toHaveBeenCalled();
    }));

    it('When submit the valid form and error returned back from payment service Then form is valid and payment service submit function is called and showError is true', async(() => {
        spyOn(paymentService, 'submit').and.returnValue(Observable.throw({ errors: [{ message: "error" }] }));

        let vm = new PaymentDetails();
        vm.bsb = 123456;
        vm.accountNumber = 12345678;
        vm.accountName = "testaccountname";
        vm.amount = 100;
        component.form.setValue(vm);

        component.submit();

        expect(component.form.valid).toBeTruthy();
        expect(component.triedToSubmitForm).toBeTruthy();
        expect(component.showSuccess).toBeFalsy();
        expect(component.showError).toBeTruthy();
        expect(paymentService.submit).toHaveBeenCalled();
    }));

});
