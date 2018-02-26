import { Injectable, Inject, isDevMode } from '@angular/core';
import { Http, RequestOptions, Headers } from '@angular/http';
import { Severity } from '../enums/severity';

@Injectable()
export class LoggerService {
    constructor(private http: Http, @Inject('BASE_URL') private baseUrl: string) {
    }

    public Log(message: string, severity: Severity) {
        if (isDevMode) {
            console.log("Log [" + Severity[severity] + "]: " + message);
        }

        this.http.post(this.baseUrl + 'api/webbrowserlog', JSON.stringify({ message: message, severity: severity }),
            new RequestOptions({ headers: new Headers({ 'Content-Type': 'application/json' }) }))
            .map((data) => data.json())
            .subscribe(
            data => {
            }, (err) => {
            });
    }
}