import { Component, ViewChild } from '@angular/core';
import { Platform, Nav } from 'ionic-angular';
import { StatusBar, Splashscreen } from 'ionic-native';
import { App, MenuController } from 'ionic-angular';
import { TabsPage } from '../pages/tabs/tabs';
import { ProfilePage } from '../pages/profile/profile';
import { AppService } from '../providers/app.service';
import { EmployeeService } from '../providers/employee.service';

@Component({
    templateUrl: 'app.html'
})
export class MyApp {
    @ViewChild(Nav) nav: Nav;
    rootPage = TabsPage;
    pages: Array<{ img: String, title: String, component: Component }>;
    user: any;

    constructor(public platform: Platform, app: App, menu: MenuController, public appService: AppService, public employeeService: EmployeeService) {
        menu.enable(true);
        this.pages = [
            { title: 'My Profile', component: ProfilePage, img: "" },
            { title: 'Sign Out', component: null, img: "" }
        ];
        this.initialize();
        this.employeeService.getProfile().subscribe(data => {this.user = data; }, error => { console.log(error); });
    }

    openPage(page) {
        if (page.title == 'My Profile') {
            this.nav.push(page.component);
        } else {
            this.appService.signOut();
        }
    }

    initialize() {
        this.platform.ready().then(() => {
            StatusBar.styleDefault();
            Splashscreen.hide();
        });
        
    }
}
