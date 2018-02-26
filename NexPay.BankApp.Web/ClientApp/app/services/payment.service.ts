import { Injectable, Inject } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { PaymentDetails } from '../viewmodels/paymentdetails';

@Injectable()
export class PaymentService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
    }

    submit(paymentDetails: PaymentDetails): Observable<any> {
        return this.http.post(this.baseUrl + 'api/payment', JSON.stringify(paymentDetails),
            new RequestOptions({ headers: new Headers({ 'Content-Type': 'application/json' }) }))
            .map((data) => data.json());
    }
}