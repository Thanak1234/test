import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { HttpUtil } from './http-util';

@Injectable()
export class AppService {

    constructor(public http: Http, public httpUtil: HttpUtil) {

    }

    getMenus() {
        return this.httpUtil.get('/lookup/menu');
    }
    signOut() {
        this.http.get('/security/logout').subscribe(
            resp => {
                window.location.reload();
            }, e => {
                window.location.reload();
            }
        );
    }
}
