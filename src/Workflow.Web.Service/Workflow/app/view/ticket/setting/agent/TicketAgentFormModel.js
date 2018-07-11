Ext.define('Workflow.view.ticket.setting.agent.TicketAgentFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-agent-ticketagentform',
    data: {                        
        form: {
            'id': null,
            'empId': null,
            'accountType': null,
            'status': null,
            'groupPolicy': null,
            'dept': null,                        
            'description': null,
            'createdDate': null,
            'modifiedDate': null,
            'employee': null            
        },
        empId: 0
    },
    formulas:{        
        isEdit: function (get) {
            return  get('id')!=null;
        }
    },
    stores: {
        // accountTypeStore: {
        //     model: 'Workflow.model.ticket.TicketLookup',
        //     proxy:{
        //         type: 'rest',
        //         url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/accounttype-list',
        //         reader: {
        //             type: 'json',
        //             rootProperty: 'data',
        //             totalProperty: 'totalCount'
        //         }
        //     }
        // },
        statusStore: {
            model: 'Workflow.model.ticket.TicketLookup',
            proxy:{
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/status-list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        groupPolicyStore:{
            model: 'Workflow.model.ticket.TicketGroupPolicy',
            proxy:{
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/lookup/grouppolicy-list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        departmentStore:{
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
        ticketSettingAgentTeamsStore: {
            model: 'Workflow.model.ticket.TicketTeam',
            autoLoad: true,            
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/agent/team-list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }

});
