import { Component } from '@angular/core';
import { NavController, NavParams, LoadingController } from 'ionic-angular';
import {DomSanitizer} from '@angular/platform-browser';
import { GlobalConfig } from '../../providers/config';

@Component({
    selector: 'page-flow',
    templateUrl: 'flow.html'
})
export class FlowPage {
    item: any;
    folio: string;
    viewUrl: any;
    protected loader: any;
    constructor(
        public navCtrl: NavController,
        public navParams: NavParams,
        private sanitizer: DomSanitizer,
        public configuration: GlobalConfig,
        public loadingCtrl: LoadingController
    ) {
        this.item = navParams.data.item;
        this.folio = navParams.data.folio;
        this.viewUrl = this.sanitizer.bypassSecurityTrustResourceUrl(configuration.baseFormUrl + navParams.data.viewUrl);
    }

    ionViewDidLoad() {

    }

    onLoaded() {
        this.presentLoading();
        var me = this;
        window.document.addEventListener('onLoadCompleted', onLoadCompleted, false)
        function onLoadCompleted(e) {
            me.dismissLoading();
        }
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
