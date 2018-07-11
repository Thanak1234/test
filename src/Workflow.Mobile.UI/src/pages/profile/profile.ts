import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { NavController, NavParams, ActionSheetController, LoadingController, AlertController, ToastController, ModalController} from 'ionic-angular';
import { EmployeeService } from '../../providers/employee.service';
import { Base } from '../../commons/base.component';

@Component({
    selector: 'page-profile',
    templateUrl: 'profile.html'
})
export class ProfilePage extends Base {
    info: any;
    constructor(
        public navCtrl: NavController,
        public navParams: NavParams,
        public employeeService: EmployeeService,
        public actionSheetCtrl: ActionSheetController,
        public loadingCtrl: LoadingController,
        public http: Http,
        private alertCtrl: AlertController,
        public toastCtrl: ToastController,
        public modalCtrl: ModalController
    ) {
        super(navCtrl, http, actionSheetCtrl, loadingCtrl, toastCtrl);
    }

    ionViewDidLoad() {
        this.presentLoading();
        this.employeeService.getProfile().subscribe(
            data => {
                this.info = data;                
                this.dismissLoading();
            }, e => {
                console.log(e);
                this.dismissLoading();
            }
        );
    }

}
