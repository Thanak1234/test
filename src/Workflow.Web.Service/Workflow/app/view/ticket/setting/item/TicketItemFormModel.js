Ext.define('Workflow.view.ticket.setting.item.TicketItemFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-item-ticketitemform',
    data: {
        form: {
            'id': null,
            'subCate': null,
            'itemName': null,
            'team': null,
            'slaId': null,
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
        ticketTeamStore: {
            model: 'Workflow.model.ticket.TicketTeam',
            autoLoad: false,
            proxy:{
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/team-list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        ticketSubCateStore: {
            model: 'Workflow.model.common.GeneralLookup',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/sub-cate',
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
