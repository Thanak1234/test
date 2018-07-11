import { AppService } from '../../providers/app.service';
import { Base } from '../../commons/base.component';
import { Component } from '@angular/core';
import { ActionSheetController, AlertController, NavController, ModalController } from 'ionic-angular';
import 'rxjs/add/operator/map';
import { Http } from '@angular/http';
import { LoadingController } from 'ionic-angular';
import { ToastController } from 'ionic-angular';
import { GlobalConfig } from '../../providers/config';

@Component({
    selector: 'page-about',
    templateUrl: 'about.html'
})
export class FormsPage extends Base {

    protected menus: any = [];

    constructor(
        public navCtrl: NavController,
        protected appService: AppService,
        public actionSheetCtrl: ActionSheetController,
        public http: Http,
        public loadingCtrl: LoadingController,
        public toastCtrl: ToastController,
        private alertCtrl: AlertController,
        public modalCtrl: ModalController,
        public config: GlobalConfig
    ) {
        super(navCtrl, http, actionSheetCtrl, loadingCtrl, toastCtrl);
    }

    ionViewDidLoad() {
        this.presentLoading();
        this.appService.getMenus().subscribe(
            data => {
                let tempMenus = data;
                if (tempMenus.length > 0) {
                    for (let t of tempMenus) {
                        if (t.text == 'Dashboard' || t.text == 'Setup' || t.text == 'Reports') continue;
                        this.menus.push(t);
                    }
                }
                this.dismissLoading();
            },
            e => {
                this.dismissLoading();
                this.toast(e);
            }
        );
    }

    openNavDetailsPage(item) {
        let url = '#' + item.routeId;
        console.log(url);
        let params = {
            item: item,
            canTakeAction: false,
            formViewUrl: url,
            folio: item.text
        };
        this.openFormPage(params);
    }
}
