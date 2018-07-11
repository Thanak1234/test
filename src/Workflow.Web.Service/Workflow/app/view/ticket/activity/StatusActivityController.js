/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.activity.StatusActivityController', {
    extend: 'Workflow.view.ticket.activity.ActivityController',
    alias: 'controller.ticket-activity-statusactivity',

    init: function () {

        var me = this;
        var vm = me.getView().getViewModel();
        var ticket = vm.get('ticket');
        me.loadInitData(ticket, function (data) {
            var status = Ext.create('Workflow.model.ticket.TicketStatus', data.status);
            vm.set('statusVal', status);
            var minutes = ticket.get('actualMinutes');
            var hours = Math.floor(minutes/60);
            var minutes = minutes % 60;
            vm.set('actualHours', hours);
            vm.set('actualMinutes', minutes);
            vm.set('rootCauseId', 0);
            if(data.subTicket && data.subTicket.length > 0){
                vm.set('hasSubtickets', true);
                var subTicketStore = vm.getStore('subTicketStore');    
                subTicketStore.setData(data.subTicket);
            }

            if(data.k2Integrated){
                vm.set('k2IntegratedData', data.k2Integrated);
                var msg = "";
                if(data.k2Integrated.clossable)
                {
                    msg = Ext.String.format("Close K2 Form #{0}", data.k2Integrated.folio);
                }else{
                    msg = Ext.String.format("K2 Form #{0} cannot be closed since it is allocated by {1}.", data.k2Integrated.folio, data.k2Integrated.allocatedUser);
                }
                vm.set('k2IntegrationMsg', msg);
                vm.set('canTriggerK2Action', data.k2Integrated.clossable);
                vm.set('closeK2Form', data.k2Integrated.clossable);
            }
        });
    },

    getMoreData: function () {
        var me = this;
        var vm = me.getView().getViewModel();

        var minutes = vm.get('actualMinutes') || 0;
        var hours = vm.get('actualHours') || 0;

        minutes = hours*60 + minutes;

        return {
            statusId: vm.get('statusVal').getId(),
            actualMinutes: minutes,
            closeK2Form: vm.get('closeK2Form'),
            rootCauseId: vm.get('rootCauseId')
        }
    },

    onMinuteCalcHandler : function(){
      var vm = this.getRef().vm;
      var minutes = vm.get('actualMinutes') || 0;
      var curHours = vm.get('actualHours') || 0;
      var hours = Math.floor(minutes / 60);

      curHours +=hours;
      minutes = minutes % 60;
      vm.set('actualHours',curHours );
      vm.set('actualMinutes', minutes );
    },

    loadInitData: function (ticket, fn) {
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket/current-status',
            method: 'GET',
            params: { ticketId: ticket.getId()},
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                if (fn) {
                    fn(data);
                }
            },
            failure: function (response) {
                console.log('error');
            }
        });
    },
    validate: function () {
        var me = this;
        var vm = me.getView().getViewModel();
        var minutes = vm.get('actualMinutes');
        var curHours = vm.get('actualHours');
        var status = vm.get('statusVal');
        var stateId = status.get('stateId');

        if (stateId == 2 && me.isEmpty(minutes) && me.isEmpty(curHours)) {
            Ext.MessageBox.show({
                title: 'Warning',
                msg: 'Please input hours or minutes.',
                icon: Ext.MessageBox.WARNING,
                buttons: Ext.MessageBox.OK
            });
            return false;
        }
        return true;
    },
    isEmpty: function (o) {
        if (o == null || o == undefined || o == 0)
            return true;
        return false;
    }
});
