Ext.define('Workflow.view.reports.procInst.ProcessInstanceReportModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.report-processinstancereport',
    data: {
        hasValue: false
    },
    formulas: {
        init: function (get) {
            if (!get('hasValue')) {
                var identity = Workflow.global.UserAccount.identity;
                var employeeId = identity?identity.id:0;
            
                var criteria = {
                    Folio: null,
                    ParticipationType: null,
                    AppName: null,
                    SubmittedDateStarted: null,
                    SubmittedDateEnded: null,
                    ParticipatedDateStarted: null,
                    ParticipatedDateEnded: null,
                    deptList: null,
                    EmployeeId: employeeId,
                    Status: null,
                    Action: null
                };
                //var store = this.storeInfo['report'];
                var criteria = this.buildParam(criteria);
                var showField = this.buildConfig({
                    Folio: true,
                    ParticipationType: true,
                    AppName: (criteria.AppName ? false : true),
                    SubmittedDateStarted: true,
                    SubmittedDateEnded: true,
                    ParticipatedDateStarted: true,
                    ParticipatedDateEnded: true,
                    deptList: true,
                    Employee: true,
                    Status: true,
                    Action: true
                });
                var data = {
                    criteria: criteria,
                    showField: showField,
                    showDept: false,
                    showEmp: true,
                    isAdmin: false,
                    empDept: 'EMP',
                    hasValue: true
                };

                this.setData(data);

                var fields = this.buildModel([]);
                var modelClassName = '_' + this.$className;
                if (!Ext.ClassManager.isCreated(modelClassName)) {
                    Ext.define(modelClassName, {
                        extend: 'Workflow.model.reports.ProcInst',
                        fields: fields
                    });
                }
            }
        }
    },
    buildConfig: function(config) {
        return config;
    },
    buildModel: function (fields) {
        return fields;
    },
    buildParam: function (criteria) {
        return criteria;
    },
    stores:{
        report: {
            //model: 'Workflow.model.reports.ProcInst',
            autoLoad: false,
            //pageSize: 2,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/processinstant',
                reader: {
                    type: 'json',
                    rootProperty: 'result',
                    totalProperty: 'count'
                }
            },
            render: function (type) {
                var me = this;
                var param = me.getProxy().extraParams;

                param.exportType = type;
                window.location.href =
                    Workflow.global.Config.baseUrl +
                    (me.getProxy().url) + '/export?' +
                    me.serialize(param);
            },
            serialize: function (obj) {
                var str = [];
                for (var p in obj) {
                    if (obj.hasOwnProperty(p)) {
						var value = obj[p];
						if(Object.prototype.toString.call(obj[p]) === '[object Date]'){
							value = Ext.util.Format.date(obj[p], 'Y-m-d');
						}
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(value));
                    }
                }
                return str.join("&");
            }
        },
        empDeptStore: Ext.create('Ext.data.Store', {
            fields: ['type', 'displayName'],
            data: [
                { "type": "EMP", "displayName": "EMPLOYEE" },
                { "type": "DEPT", "displayName": "DEPARTMENT (S)" }
            ]
        }),
        departmentStore: {
            model: 'Workflow.model.common.Department',
            autoLoad: false,
            proxy: {
                type: 'rest',

                url: Workflow.global.Config.baseUrl + 'api/employee/department-right',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            } 
        },
        appStore: {
            model: 'Workflow.model.common.worklists.Process',
            autoLoad: true
        }
    }
});