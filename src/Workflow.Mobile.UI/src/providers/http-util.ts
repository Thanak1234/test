import { Injectable } from '@angular/core';
import { Http, URLSearchParams, Headers, RequestOptions, Response } from '@angular/http';
import 'rxjs/add/operator/map';
import { GlobalConfig } from './config';
import {Observable} from 'rxjs/Observable';

@Injectable()
export class HttpUtil {
    constructor(public http: Http, public config: GlobalConfig) {
    }
    get(url: string, params: any=null) {        
        let apiUrl: string;
        if (!this.isEmpty(params)) { let queryStr = this.getParams(params); apiUrl = this.config.baseUrl + url + '?' + queryStr; }
        else apiUrl = this.config.baseUrl + url;
        let promise = this.http.get(apiUrl)
            .map(res => res.json());
        return promise;
    }
    isEmpty(param) {
        if (param == undefined || param == null)
            return true;
        return false;
    }
    getParams(obj: any) {
        let params: URLSearchParams = new URLSearchParams();
        for (var key in obj) {
            if (obj.hasOwnProperty(key)) {
                var element = obj[key];
                params.set(key, element);
            }
        }
        return params;
    }

    post(url: string, data?: any) {
        let body = data != null ? JSON.stringify(data) : {};
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let apiUrl = this.config.baseUrl + url;
        return this.http.post(apiUrl, body, options);
    }
}