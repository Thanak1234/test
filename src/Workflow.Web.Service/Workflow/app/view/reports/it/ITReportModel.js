Ext.define('Workflow.view.reports.it.ITReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-it',
    buildConfig: function (showField) {
        showField.AppName = false;
        return showField;
    },
    buildParam: function (criteria) {        

        return criteria;
    },
    buildModel: function (fields) {
        fields = [
             
        ]
        return fields;
    },
    stores: {
        sessionStore : { 
            model : 'Workflow.model.itRequestForm.Session',
            data  : [
                { "id": 3, "sessionName": "INFRASTRUCTURE" },
                { "id": 1, "sessionName": "CASINO APPLICATION" },
                { "id": 2, "sessionName": "HOTEL APPLICATION" }
            ]
        },
        itemStore: {
            model: 'Workflow.model.itRequestForm.Item',
            pageSize: 20,
            autoLoad: false,

            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/itrequestitem/items-incl-deprecated',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        itemTypeStore: {
            model: 'Workflow.model.itRequestForm.ItemType',

            pageSize: 20,

            autoLoad: false,

            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/itrequestitem/itemtypes',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        itemRoleStore: {
            model: 'Workflow.model.itRequestForm.ItemRole',
            pageSize: 20,
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/itrequestitem/itemroles',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }
});