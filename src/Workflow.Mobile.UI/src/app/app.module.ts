import { NgModule, ErrorHandler } from '@angular/core';
import { IonicApp, IonicModule, IonicErrorHandler } from 'ionic-angular';
import { MyApp } from './app.component';
import { FormsPage } from '../pages/about/about';
import { ContactPage } from '../pages/contact/contact';
import { HomePage } from '../pages/home/home';
import { TabsPage } from '../pages/tabs/tabs';
import { DetailPage } from '../pages/detail/detail';
import { GlobalConfig } from '../providers/config';
import { DashboardService } from '../providers/dashboard.service';
import { FlowPage } from '../pages/flow/flow';
import { EmployeePage } from '../pages/employee/employee';
import { HttpUtil } from '../providers/http-util';
import { EmployeeService } from '../providers/employee.service';
import { AppService } from '../providers/app.service';
import { ProfilePage } from '../pages/profile/profile';

@NgModule({
    declarations: [
        MyApp,
        FormsPage,
        ContactPage,
        HomePage,
        TabsPage,
        DetailPage,
        FlowPage,
        EmployeePage,
        ProfilePage
    ],
    imports: [
        IonicModule.forRoot(MyApp)
    ],
    bootstrap: [IonicApp],
    entryComponents: [
        MyApp,
        FormsPage,
        ContactPage,
        HomePage,
        TabsPage,
        DetailPage,
        FlowPage,
        EmployeePage,
        ProfilePage
    ],
    providers: [
        { provide: ErrorHandler, useClass: IonicErrorHandler },
        GlobalConfig,
        DashboardService,
        HttpUtil,
        EmployeeService,
        AppService
    ]
})
export class AppModule { }
