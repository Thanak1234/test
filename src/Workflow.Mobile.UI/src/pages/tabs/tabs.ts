import { Component } from '@angular/core';

import { HomePage } from '../home/home';
import { FormsPage } from '../about/about';
import { ContactPage } from '../contact/contact';
import { App, MenuController } from 'ionic-angular';

@Component({
  templateUrl: 'tabs.html'
})
export class TabsPage {
  // this tells the tabs component which Pages
  // should be each tab's root Page
  tab1Root: any = HomePage;
  tab2Root: any = FormsPage;
  tab3Root: any = ContactPage;

  constructor(app: App, menu: MenuController) {
    menu.enable(true);
  }
}
