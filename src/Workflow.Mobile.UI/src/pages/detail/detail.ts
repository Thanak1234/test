import { Component, ElementRef } from '@angular/core';
import { ActionSheetController } from 'ionic-angular';
import { DashboardService } from '../../providers/dashboard.service';
import 'rxjs/add/operator/map';
import { Http } from '@angular/http';
import { LoadingController } from 'ionic-angular';
import { NavController, NavParams, AlertController, ModalController} from 'ionic-angular';
import { DomSanitizer } from '@angular/platform-browser';
import { GlobalConfig } from '../../providers/config';
import { Base } from '../../commons/base.component';
import { ToastController } from 'ionic-angular';
import { EmployeePage } from '../employee/employee';

@Component({
    selector: 'page-detail',
    templateUrl: 'detail.html'
})
export class DetailPage {
    item: any;
    formUrl: any = '';
    canTakeAction: boolean = false;
    folio: string = '';
    protected loader: any;
    constructor(
        public navCtrl: NavController,
        public actionSheetCtrl: ActionSheetController,
        private service: DashboardService,
        public http: Http,
        public loadingCtrl: LoadingController,
        public navParams: NavParams,
        private sanitizer: DomSanitizer,
        public configuration: GlobalConfig,
        public toastCtrl: ToastController,
        private alertCtrl: AlertController,
        public modalCtrl: ModalController
    )
    {
        this.item = navParams.data.item;
        this.canTakeAction = navParams.data.canTakeAction;
        var formViewUrl = navParams.data.formViewUrl;
        this.folio = navParams.data.folio;
        this.formUrl = this.sanitizer.bypassSecurityTrustResourceUrl(configuration.baseFormUrl + formViewUrl);
        this.presentLoading();
    }

    onLoaded() {
        var me = this;
        window.document.addEventListener('onLoadCompleted', onLoadCompleted, false)
        function onLoadCompleted(e) {
            me.dismissLoading();
        }
    }

    onActionClick() {
        let buttons = [];
        let actions = this.item.Actions;
        for (let action of actions) {
            buttons.push({
                text: action.Name,
                handler: () => {
                    let confirm = this.alertCtrl.create({
                        title: 'Confirm Action',
                        message: 'Are you sure?',
                        buttons: [
                            {
                                text: 'Yes',
                                handler: () => {                                    
                                    let params = {
                                        serialNumber: this.item.Serial,
                                        actionName: action.Name,
                                        sharedUser: this.item.SharedUser,
                                        managedUser: this.item.ManagedUser
                                    };
                                    this.presentLoading();
                                    this.service.takeAction(params).subscribe(
                                        resp => {
                                            this.dismissLoading();
                                            this.navCtrl.pop();
                                        },
                                        e => {
                                            this.dismissLoading();
                                            this.toast(e);
                                        }
                                    );

                                }
                            },
                            {
                                text: 'No',
                                role: 'cancel',
                                handler: () => {
                                    this.toast('cancel.');
                                }
                            }
                        ]
                    });
                    confirm.present();
                }
            });
        }

        if (buttons.length > 0) {
            this.presentActionSheet({
                buttons: buttons
            });
        }
    }

    onMoreClick() {
        let buttons = [];
        let status = this.item.Status;

        if (status == 'Available') {
            buttons = [
                {
                    text: 'Share',
                    handler: () => {
                        this.presentShare(this.item);
                    }
                }, {
                    text: 'Redirect',
                    handler: () => {
                        this.presentRedirect(this.item);
                    }
                }
            ];
        } else if (status == 'Open') {
            buttons = [
                {
                    text: 'Share',
                    handler: () => {
                        this.presentShare(this.item);
                    }
                }, {
                    text: 'Redirect',
                    handler: () => {
                        this.presentRedirect(this.item);
                    }
                }, {
                    text: 'Release',
                    handler: () => {
                        this.release(this.item);
                    }
                }
            ];
        } else if (status == 'Allocated') {
            buttons = [
                {
                    text: 'Release',
                    handler: () => {
                        this.release(this.item);
                    }
                }
            ];
        }

        if (buttons.length > 0) {
            this.presentActionSheet({
                buttons: buttons
            });
        }
    }

    presentShare(item) {
        let modal = this.modalCtrl.create(EmployeePage, { title: 'Share' });
        modal.onDidDismiss(data => {
            if (data) {
                this.service.share(item.Serial, [data]).subscribe(
                    resp => {
                        this.toast('success.');
                        this.navCtrl.pop();
                    }, error => {
                        console.log('presentShare called -> ', error);
                    });
            }
        });
        modal.present();
    }

    presentRedirect(item) {
        let modal = this.modalCtrl.create(EmployeePage, { title: 'Redirect' });
        modal.onDidDismiss(data => {
            if (data) {
                this.service.redirect(item.Serial, data).subscribe(
                    resp => {
                        this.toast('success.');
                        this.navCtrl.pop();
                    }, error => {
                        console.log('presentRedirect called -> ', error);
                    });
            }
        });
        modal.present();
    }

    release(item) {
        if (item.Status == 'Allocated') {
            this.service.adminRelease(item.Serial).subscribe(
                resp => {
                    this.toast('success.');
                    this.navCtrl.pop();
                }, e => {
                    console.log(e);
                }
            );
        } else if (item.Status == 'Open') {
            let params = {
                serialNumber: item.Serial,
                sharedUser: item.SharedUser,
                managedUser: item.ManagedUser
            };
            this.service.ownRelease(params).subscribe(
                resp => {
                    this.toast('success.');
                    this.navCtrl.pop();
                }, e => {
                    console.log(e);
                }
            );
        }
    }

    toast(msg: string) {
        let toast = this.toastCtrl.create({
            message: msg,
            duration: 1500,
            position: 'top'
        });
        toast.present();
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

}
