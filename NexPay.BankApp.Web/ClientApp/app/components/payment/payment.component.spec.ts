/// <reference path="../../../../node_modules/@types/jasmine/index.d.ts" />
import { assert } from 'chai';
import { TestBed, async, ComponentFixture } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms'
import { HttpModule } from '@angular/http';
import { Observable } from 'rxjs';

import { PaymentComponent } from './payment.component';

let fixture: ComponentFixture<PaymentComponent>;
let component: PaymentComponent;

describe('Payment component', () => {
    
    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [HttpModule, ReactiveFormsModule],
            providers: [{ provide: 'BASE_URL', useFactory: () => "TestBaseUrl" }]
        });
        fixture = TestBed.createComponent(PaymentComponent);
        component = fixture.componentInstance;
    });   
});
