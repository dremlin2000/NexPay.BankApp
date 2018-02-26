import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { PaymentComponent } from './components/payment/payment.component';

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
        RouterModule.forRoot([
            { path: '', redirectTo: 'payment', pathMatch: 'full' },
            { path: 'payment', component: PaymentComponent },
            { path: '**', redirectTo: 'payment' }
        ])
    ]
})
export class AppModuleShared {
}
