/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.AssignedActivityController', {
    extend: 'Workflow.view.ticket.activity.ActivityController',
    alias: 'controller.ticket-activity-assignedactivity',


    init: function () {
        var me = this;
        var vm = me.getView().getViewModel();
        var ticketTeamStore = vm.getStore('ticketTeamStore');
        var ticketAgentSore = vm.getStore('ticketAgentStore');
        me.ticket = vm.get('ticket');
        ticketTeamStore.load( {

            scope: this,
            callback: function(records, operation, success) {
               me.initData(records, function(data){
                    ticketAgentSore.getProxy().extraParams = { teamId: data.teamId };
                    ticketAgentSore.load({
                        scope: this,
                        callback: function(records, operation, success) {
                            var item = records.find(function(t){
                                return t.id === data.agentId
                            });
                            vm.set('agent', item);
                        } 
                    });
               });
                
            }
        } );

        
    },

    initData : function(records, next){
        var me = this;
        var vm = this.getView().getViewModel();
        var ticket = this.ticket; 
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket/current-assigned',
            method: 'GET',
            params: { ticketId: ticket.getId()},
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                var item = records.find(function(item){ 
                    return item.getId() ===  data.teamId 
                });
                vm.set('team', item);
                if(next && data.agentId){
                    next(data);
                }
            },
            failure: function (response) {
                console.log('error');
            }
        });
    },
    onTeamChanged: function () {
        var me = this;
        var vm = me.getView().getViewModel();
        var team = vm.get('team');
        var ticketAgentSore = vm.getStore('ticketAgentStore');
        if(!team){
            ticketAgentSore.setData([]);
            return;
        }

        var teamId = team.getId();
        vm.set('agent', null);
        ticketAgentSore.getProxy().extraParams = { teamId: teamId };
        ticketAgentSore.load();
    },

    getMoreData: function () {
        var me = this;
        var vm = me.getView().getViewModel();
        return {
            teamId: vm.get('team').getId(),
            assignee: vm.get('agent')==null? null:vm.get('agent').getId()
        }
    }


    
});
