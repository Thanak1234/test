import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { HttpUtil } from './http-util';

@Injectable()
export class EmployeeService {

    constructor(public http: Http, public httpUtil: HttpUtil) {
        console.log('Hello EmployeeService Provider');
    }
    getEmployees(params) {        
        return this.httpUtil.get('/employee/search', params);
    }

    getProfile() {
        return this.httpUtil.get('/employee/currentuser');
    }
}
