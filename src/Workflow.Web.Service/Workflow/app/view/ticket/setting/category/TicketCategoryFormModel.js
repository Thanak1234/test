Ext.define('Workflow.view.ticket.setting.category.TicketCategoryFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-category-ticketcategoryform',
    data: {
        form: {
            'id': null,
            'dept': null,
            'cateName': null,
            'description': null,
            'createdDate': null,
            'modifiedDate': null,
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
        statusStore: {
            model: 'Workflow.model.ticket.TicketLookup'
        }
    }

});
