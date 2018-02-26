import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PaymentComponent } from './components/payment/payment.component';

import { UtilityService } from './services/utility.service';
import { PaymentService } from './services/payment.service';
import { LoggerService } from './services/logger.service';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        PaymentComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'payment', pathMatch: 'full' },
            { path: 'payment', component: PaymentComponent },
            { path: '**', redirectTo: 'payment' }
        ])
    ],
    providers: [
        UtilityService,
        PaymentService,
        LoggerService
    ]
})
export class AppModuleShared {
}
