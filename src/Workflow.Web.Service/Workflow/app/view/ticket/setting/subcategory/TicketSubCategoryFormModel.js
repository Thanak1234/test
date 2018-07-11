Ext.define('Workflow.view.ticket.setting.subcategory.TicketSubCategoryFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-subcategory-ticketsubcategoryform',
    data: {
        form: {
            'id': null,
            'cate': null,
            'subCateName': null,
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
        ticketCateStore: {
            model: 'Workflow.model.ticket.TicketSettingCategory',
            proxy:{
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/cate-list',
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
