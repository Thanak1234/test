Ext.define('Workflow.view.ticket.setting.grouppolicy.AssignTeamFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-grouppolicy-assignteamform',

    init: function () {
        // var me = this;
        // var view = me.getView();
        // var model = view.getViewModel();
        // var refs = me.getView().getReferences();
        // var groupPolicyId = view.groupPolicyId;
        //var dataStore = view.assignedTeamStore;
        // var gridAssignedTeamStore = model.getStore('gridAssignedTeamStore');
        // gridAssignedTeamStore.setData(dataStore.getData());

    },
    onAddTeamHanler: function(){
        var me = this;
        var view = me.getView();
        var vm = me.getViewModel();
        var r = me.getReferences();
        var form = r.teamPickupFormRef;
        if(form.isValid()){
          var data = vm.get('form.team') ? vm.get('form.team').data : {};
          var store = view.assignedTeamStore;
          var existed = false;
          store.each(function(record){
              if(data.id == record.id){
                 existed = true;
                 return false;
              }
          });

          if(existed){
              Ext.MessageBox.show({
                  title: 'Validation',
                  msg: 'There was existing in Team List...',
                  buttons: Ext.MessageBox.OK,
                  icon: Ext.MessageBox.ERROR
              });
              return false;
          }

          store.add(data);
          form.reset();
        }
    },
    onTeamPickupChanged: function(queryPlan, eOpts){
        var vm = this.getView().getViewModel();
        var store = vm.getStore('ticketSettingTeamsStore');
        if (store) {
            Ext.apply(store.getProxy().extraParams, {
                query:  queryPlan.query
            });
        }
    },
    onDeleteTeamHandler: function(grid, rowIndex, columnIndex, opt){
        var me = this, view = me.getView(), model = me.getView().getViewModel();
        Ext.Msg.confirm ('Confirmation', 'Are you sure to remove?' , function(btn, text){
             if (btn == 'yes') {
                var store = view.assignedTeamStore;
                var record = store.getAt(rowIndex);
                store.removeAt(rowIndex);
             }
        });
    },
    onFormSubmit: function(){
      var me = this, view=me.getView();
      var vm = me.getView().getViewModel();
      var store = view.assignedTeamStore;
      me.getView().cbFn(store, view);
    },
    onCloseClick: function (btn, e) {
        var me = this;
        var v = me.getView();
        v.close();
    }
});
