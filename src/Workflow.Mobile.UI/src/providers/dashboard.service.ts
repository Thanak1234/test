import { Injectable } from '@angular/core';
import { Http, URLSearchParams  } from '@angular/http';
import 'rxjs/add/operator/map';
import { HttpUtil } from './http-util';

@Injectable()
export class DashboardService {
    constructor(public http: Http, public httpUtil: HttpUtil) {}
    getWorklistItems() {
        return this.httpUtil.get('/worklists', null);
    }

    getContributeItems(params) {
        return this.httpUtil.get('/dashboard/tasks/contributed', params);
    }

    getSubmittedItems(params) {
        return this.httpUtil.get('/dashboard/tasks/submitted', params);
    }

    takeAction(params) {
        return this.httpUtil.get('/worklists/executeaction', params);
    }

    share(serialNumber: string, data: any) {
        return this.httpUtil.post('/worklists/setsharedusers?serialNumber=' + serialNumber, data); 
    }

    redirect(serialNumber: string, data: any) {
        return this.httpUtil.post('/worklists/redirect?serialNumber=' + serialNumber, data); 
    }

    ownRelease(params) {        
        return this.httpUtil.get('/worklists/release', params);
    }

    adminRelease(serialNumber) {
        return this.httpUtil.post(`/worklists/forcerelease/${serialNumber}`);
    }
}
