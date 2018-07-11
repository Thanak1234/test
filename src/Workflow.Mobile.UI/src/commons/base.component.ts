import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { ActionSheetController } from 'ionic-angular';
import { LoadingController } from 'ionic-angular';
import { DetailPage } from '../pages/detail/detail';
import { FlowPage } from '../pages/flow/flow';
import { NavController } from 'ionic-angular';
import { ToastController } from 'ionic-angular'

export class Base {    
    protected loader: any;
    constructor(
        public navCtrl: NavController,
        public http: Http,
        public actionSheetCtrl: ActionSheetController,
        public loadingCtrl: LoadingController,
        public toastCtlr: ToastController) {
        console.log('Hello Config Provider');
    }
    presentActionSheet(config) {
        let actionSheet = this.actionSheetCtrl.create(config);
        actionSheet.present();
    }

    presentLoading() {
        this.loader = this.loadingCtrl.create({
            content: "Please wait..."
        });
        this.loader.present();
    }

    dismissLoading() {
        this.loader.dismissAll();
    }

    openFormPage(params) {
        this.navCtrl.push(DetailPage, params);
    }

    openViewFlowPage(params) {
        this.navCtrl.push(FlowPage, params);
    }

    toast(msg: string) {
        let toast = this.toastCtlr.create({
            message: msg,
            duration: 1500,
            position: 'top'
        });
        toast.present();
    }
}