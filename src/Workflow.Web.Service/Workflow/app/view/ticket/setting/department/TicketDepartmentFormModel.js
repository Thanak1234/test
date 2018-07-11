Ext.define('Workflow.view.ticket.setting.department.TicketDepartmentFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-department-ticketdepartmentform',
    data: {
        form: {
            'id': null,
            'deptName': null,
            'automationEmail': null,
            'description': null,
            'createdDate': null,
            'modifiedDate': null,
            'item': null ,
            'status': null
        }
    },
    formulas:{
        isEdit: function (get) {
            return  get('id')!=null;
        }
    },
    stores: {
        ticketDeptStore: {
            model: 'Workflow.model.ticket.TicketDepartment',
            proxy:{
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/dept-list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        defaultItemStore: {
            model: 'Workflow.model.ticket.TicketSettingItem',
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/getItems',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        statusStore: {
            model: 'Workflow.model.ticket.TicketLookup'
        }
    }

});
