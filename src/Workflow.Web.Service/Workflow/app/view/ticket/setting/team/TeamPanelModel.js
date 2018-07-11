Ext.define('Workflow.view.ticket.setting.team.TeamPanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-setting-team-team',
    data: {
        name: 'Workflow',
        query: null,
        status: 'ACTIVE',
        columnDateWidth: 135,
        columnStatusWidth: 75
    },
    stores: {
        ticketSettingTeamStore: {
            model: 'Workflow.model.ticket.TicketTeam',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ticket/setting/team/list',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }
});
