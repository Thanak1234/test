import { Component } from '@angular/core';
import { NavController, NavParams, ViewController } from 'ionic-angular';
import { EmployeeService } from '../../providers/employee.service';

@Component({
    selector: 'page-employee',
    templateUrl: 'employee.html'
})
export class EmployeePage {
    public employees: Array<any> = [];
    public searchQuery: string = '';
    public title: string = '';

    constructor(
        public navCtrl: NavController,
        public navParams: NavParams,
        public viewCtrl: ViewController,
        public employeeService: EmployeeService
    ) {
        this.title = navParams.data.title;
    }

    ionViewDidLoad() {
    }

    onSelected(item) {
        this.viewCtrl.dismiss(item);
    }

    onSearching(evt) {
        if (this.searchQuery.length == 0) {
            this.employees = [];
        }
        if (this.searchQuery.length > 3) {
            var params = {
                query: this.searchQuery,
                integrated: true,
                excludeOwner: true,
                includeInactive: false,
                includeGenericAcct: false,
                page: 1,
                start: 0,
                limit: 20
            };
            this.employeeService.getEmployees(params)
                .subscribe(
                data => {
                    this.employees = data.data;
                }, error => {
                });
        }
    }

    onSearchCancel(evt) {
        this.employees = [];
    }

    loadMore(evt) {
        var params = {
            query: this.searchQuery,
            integrated: true,
            excludeOwner: true,
            includeInactive: false,
            includeGenericAcct: false,
            page: 1,
            start: 0,
            limit: this.employees.length + 20
        };
        this.employeeService.getEmployees(params)
            .subscribe(
            data => {
                this.employees = data.data;
            }, error => {
            });
    }

    dismiss() {
        this.viewCtrl.dismiss();
    }

}
