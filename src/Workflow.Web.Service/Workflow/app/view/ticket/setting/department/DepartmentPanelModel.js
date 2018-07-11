Ext.define('Workflow.view.ticket.setting.department.DepartmentPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-department-department',
    data: {
        name: 'Workflow',
        query: null,
        status: 'ACTIVE',
        columnDateWidth: 135,
        columnStatusWidth: 75
    },
    stores: {
        ticketSettingDepartmentStore: {
            model: 'Workflow.model.ticket.TicketDepartment',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/dept/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }
});
