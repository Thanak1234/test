Ext.define('Workflow.view.ticket.setting.team.RegisteredAgentWindowController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.ticket-setting-team-registeredagentwindowcontroller',
    onCloseClick: function (btn, e) {
        var me = this;
        var v = me.getView();
        v.close();
    },
    init: function(){
        var me = this;
        var model = me.getView().getViewModel();
    },
    onAddAgentToTeamHanler: function(){
        var me = this;
        var vm = me.getViewModel();
        var r = me.getReferences();
        var form = r.agentPickupFormRef;        
        var data = vm.get('form.agent').data;
        data.immediateAssign  = vm.get('form.immediateAssign');
        me.getView().cbFn(data, form);
    },
    onAgentPickupChanged: function(queryPlan, eOpts){
        var vm = this.getView().getViewModel();
        var store = vm.getStore('agentStore');
        if (store) {
            Ext.apply(store.getProxy().extraParams, {
                query:  queryPlan.query
            });
        }
    }
});

