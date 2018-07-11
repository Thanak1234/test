import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
@Injectable()
export class GlobalConfig {

    public baseUrl: string = "/api";
    public baseFormUrl: string = 'http://forms.nagaworld.dev/?mobile=true&singleForm=true';

    constructor(public http: Http) {
        console.log('Hello Config Provider');
    }

}
