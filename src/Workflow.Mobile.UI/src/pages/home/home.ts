import { Component } from '@angular/core';
import { ActionSheetController, AlertController, NavController, ModalController } from 'ionic-angular';
import { DashboardService } from '../../providers/dashboard.service';
import { Base } from '../../commons/base.component';
import 'rxjs/add/operator/map';
import { Http } from '@angular/http';
import { LoadingController } from 'ionic-angular';
import { ToastController } from 'ionic-angular';
import { EmployeePage } from '../employee/employee';

@Component({
    selector: 'page-home',
    templateUrl: 'home.html'
})
export class HomePage extends Base {

    public WORKLIST: string = 'worklist';
    public CONTRIBUTED: string = 'contributed';
    public SUBMITTED: string = 'submitted';


    public worklists: Array<any>  = [];
    public contributes: Array<any> = [];
    public submitteds: Array<any> = [];
    public oWorklists: Array<any> = [];
    public tab: string = 'worklist';
    public searchQuery: string = null;

    constructor(
        public navCtrl: NavController,
        public actionSheetCtrl: ActionSheetController,
        private service: DashboardService,
        public http: Http,
        public loadingCtrl: LoadingController,
        private alertCtrl: AlertController,
        public toastCtrl: ToastController,
        public modalCtrl: ModalController
    )
    {   
        super(navCtrl, http, actionSheetCtrl, loadingCtrl, toastCtrl);
        this.tab = 'worklist';
        this.selectedWorklist();
    }

    presentOutOffice(evt) {
        let profileModal = this.modalCtrl.create(EmployeePage, { title: 'Redirect' });
        profileModal.onDidDismiss(data => {
            if (data) {

            }
        });
        profileModal.present();
    }

    selectedWorklist() {
        this.presentLoading();
        this.service.getWorklistItems().subscribe(
            data => {
                this.worklists = data.Records;
                this.oWorklists = data.Records;
                this.dismissLoading();
            }, error => { this.dismissLoading();});
    }

    selectedContributed() {

        if (this.contributes.length > 0) return;

        var params = {
            query: null,
            page: 1,
            start: 0,
            limit: 10
        };
        this.presentLoading();
        this.service.getContributeItems(params).subscribe(
            data => {
                this.contributes = data.Records;
                this.dismissLoading();
            }, error => { this.dismissLoading();});
    }

    selectedSubmitted() {
        if (this.submitteds.length > 0) return;
        var params = {
            query: null,
            page: 1,
            start: 0,
            limit: 10
        };
        this.presentLoading();
        this.service.getSubmittedItems(params).subscribe(
            data => {
                this.submitteds = data.Records;
                this.dismissLoading();
            }, error => { this.dismissLoading(); });
    }

    openViewFlow(item) {
        let params = {
            item: item,
            folio: item.folio,            
            viewUrl: item.flowUrl
        };
        this.openViewFlowPage(params);
    }

    openNavDetailsPage(item) {
        let params = {
            item: item,
            canTakeAction: true,
            formViewUrl: '',
            folio: ''
        };
        if (this.tab == this.WORKLIST) {
            params.folio = item.Folio;
            params.formViewUrl = item.ViewFromUrl;
            this.openFormPage(params);
        } else {
            params.folio = item.folio;
            params.canTakeAction = false;
            params.formViewUrl = item.formUrl;
            this.openFormPage(params);
        }
    }

    presentAction($event, item) {
        let buttons = [];
        let actions = item.Actions;
        for (let action of actions) {
            if (action.Batchable) {
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
                                            serialNumber: item.Serial,
                                            actionName: action.Name,
                                            sharedUser: item.SharedUser,
                                            managedUser: item.ManagedUser
                                        };
                                        this.service.takeAction(params).subscribe(
                                            resp => {
                                                this.selectedWorklist();
                                                this.toast('success.');
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
        };
        if (buttons.length > 0) {
            this.presentActionSheet({
                buttons: buttons
            });
        }
    }

    presentMore(item) {
        let buttons = [];
        let status = item.Status;

        if (status == 'Available') {
            buttons = [
                {
                    text: 'Share',
                    handler: () => {
                        this.presentShare(item);
                    }
                }, {
                    text: 'Redirect',
                    handler: () => {
                        this.presentRedirect(item);
                    }
                }, {
                    text: 'View Flow',
                    handler: () => {
                        let params = {
                            item: item,
                            folio: item.Folio,
                            viewUrl: item.ViewFlow
                        };
                        this.openViewFlowPage(params);
                    }
                }
            ];
        } else if (status == 'Open') {
            buttons = [
                {
                    text: 'Share',
                    handler: () => {
                        this.presentShare(item);
                    }
                }, {
                    text: 'Redirect',
                    handler: () => {
                        this.presentRedirect(item);
                    }
                }, {
                    text: 'Release',
                    handler: () => {
                        this.release(item);
                    }
                }, {
                    text: 'View Flow',
                    handler: () => {
                        let params = {
                            item: item,
                            folio: item.Folio,
                            viewUrl: item.ViewFlow
                        };
                        this.openViewFlowPage(params);
                    }
                }
            ];
        } else if (status == 'Allocated') {
            buttons = [
                {
                    text: 'Release',
                    handler: () => {
                        this.release(item);
                    }
                }, {
                    text: 'View Flow',
                    handler: () => {
                        let params = {
                            item: item,
                            folio: item.Folio,
                            viewUrl: item.ViewFlow
                        };
                        this.openViewFlowPage(params);
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
                        this.selectedWorklist();
                        this.toast('success.');
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
                        this.selectedWorklist();
                        this.toast('success.');
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
                    this.selectedWorklist();
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
                    this.selectedWorklist();
                }, e => {
                    console.log(e);
                }
            );
        }
    }

    doRefresh(refresher) {
        switch (this.tab) {
            case this.WORKLIST: {
                this.service.getWorklistItems().subscribe(
                    data => {
                        this.worklists = data.Records;
                        this.oWorklists = data.Records;
                        refresher.complete();
                    }, error => {
                        refresher.cancel();
                    });
                break;
            }
            case this.CONTRIBUTED: {
                let params = {
                    query: null,
                    page: 1,
                    start: 0,
                    limit: 10
                };
                this.service.getContributeItems(params).subscribe(
                    data => {
                        this.contributes = data.Records;
                        refresher.complete();
                    }, error => { refresher.complete(); });
                break;
            }
            case this.SUBMITTED: {
                let params = {
                    query: null,
                    page: 1,
                    start: 0,
                    limit: 10
                };
                this.service.getSubmittedItems(params).subscribe(
                    data => {
                        this.submitteds = data.Records;
                        refresher.complete();
                    }, error => { refresher.complete();});
                break;
            }
        }
    }

    loadContributed(infiniteScroll) {
        console.log('loadContributed called');
        let params = {
            query: null,
            page: 1,
            start: 0,
            limit: this.contributes.length + 10
        };
        this.service.getContributeItems(params).subscribe(
            data => {
                this.contributes = data.Records;
                infiniteScroll.complete();
            }, error => { infiniteScroll.complete();});
    }

    loadSubmitted(infiniteScroll) {
        console.log('loadSubmitted called');
        let params = {
            query: null,
            page: 1,
            start: 0,
            limit: this.submitteds.length + 10
        };
        this.service.getContributeItems(params).subscribe(
            data => {
                this.submitteds = data.Records;
                infiniteScroll.complete();
            }, error => { infiniteScroll.complete(); });
    }

    onSearching(evt) {
        switch (this.tab) {
            case this.WORKLIST: {
                if (this.worklists.length > 0 && this.searchQuery != '') {
                    let news = [];
                    this.worklists.filter((el, i, s) => {
                        if (this.contain(el.Folio, this.searchQuery)) {
                            news.push(el);
                            return el;
                        }
                    });

                    this.worklists = news;
                } else if (this.searchQuery == '') {
                    this.worklists = this.oWorklists;
                }
                break;
            }
            case this.CONTRIBUTED: {
                if (this.searchQuery.length > 5) {
                    let params = {
                        query: this.searchQuery,
                        page: 1,
                        start: 0,
                        limit: 10
                    };
                    this.presentLoading();                    
                    this.service.getContributeItems(params).subscribe(
                        data => {
                            this.contributes = data.Records;
                            this.dismissLoading();
                        }, error => { console.log(error); this.dismissLoading(); });
                }
                break;
            }
            case this.SUBMITTED: {
                if (this.searchQuery.length > 5) {
                    let params = {
                        query: this.searchQuery,
                        page: 1,
                        start: 0,
                        limit: 10
                    };
                    this.presentLoading();                    
                    this.service.getSubmittedItems(params).subscribe(
                        data => {
                            this.submitteds = data.Records;
                            this.dismissLoading();
                        }, error => { console.log(error); this.dismissLoading(); });
                }
                break;
            }
        }
    }

    contain(p1: string, p2: string): boolean {
        var v1 = p1.toLowerCase();
        var v2 = p2.toLowerCase();
        if (v1 == '' || v2 == '') return false;
        else return v1.search(p2) != -1;
    }

    onSearchCancel(evt) {
        this.selectedWorklist();
    }

    renderer(item) {
        if (item.Status == 'Available') {
            return { 'backgroundColor': 'green' };
        } else if (item.Status == 'Open') {
            return { 'backgroundColor': 'blue' };
        } else if (item.Status == 'Allocated') {
            return { 'backgroundColor': '#ffd777' };
        }
    }
}


