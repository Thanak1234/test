Ext.define('Workflow.view.ticket.setting.grouppolicy.TicketGroupPolicyFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-grouppolicy-ticketgrouppolicyform',

    init: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();

        var statusStore = model.getStore('statusStore');
        statusStore.proxy.extraParams = {lookupKey:'GROUP_POLICY_STATUS'};
        statusStore.load();
        model.getStore('newTicketNotifyStore').load({params:{lookupKey:'GROUP_POLICY_NEW_TICKET_NOTIFY'}});
        model.getStore('assignedNotifyStore').load({params:{lookupKey:'GROUP_POLICY_ASSIGNED_NOTIFY'}});
        model.getStore('replyNotifyStore').load({params:{lookupKey:'GROUP_POLICY_REPLY_NOTIFY'}});
        model.getStore('changeStatusNotifyStore').load({params:{lookupKey:'GROUP_POLICY_CHANGE_STATUS_NOTIFY'}});

        me.renderLimitAccessCustom();
        me.renderReportLimitAccessCustom();
    },

    renderLimitAccessCustom: function(){
      var me = this;
      var view = me.getView();
      var model = view.getViewModel();
      var refs = me.getView().getReferences();
      var customRecord = Ext.create('Workflow.model.ticket.TicketLookup', {
          'id': 0,
          'display': 'Custom',
          'display1': 'CUSTOM',
          'display2': null,
          'description': 'There are some teams were assigned'});
      var cmbLimitAccess = model.getStore('limitAccessStore');
      var groupPolicyId = model.get('form.id') | 0;
      var limitAccess  = model.get('form.limitAccess');
      var assignedTeamStore = model.getStore('assignedTeamStore');
      assignedTeamStore.proxy.extraParams = {groupPolicyId: groupPolicyId};
      assignedTeamStore.load({
        scope: this,
        callback: function(records, operation, success) {
            if(records.length > 0){
                cmbLimitAccess.removeAll();
                cmbLimitAccess.add(customRecord);
                model.set('customLimitAccessFocusValue', 0);
            }else{
                cmbLimitAccess.proxy.extraParams = {lookupKey:'GROUP_POLICY_LIMIT_ACCESS'};
                cmbLimitAccess.load(
                  function(recs, ope, suc){
                      if(limitAccess){
                        model.set('customLimitAccessFocusValue', limitAccess.id);
                      }
                  }
               );
            }
        }
      });

    },
    renderReportLimitAccessCustom: function(){
      var me = this;
      var view = me.getView();
      var model = view.getViewModel();
      var refs = me.getView().getReferences();
      var customRecord = Ext.create('Workflow.model.ticket.TicketLookup', {
          'id': 0,
          'display': 'Custom',
          'display1': 'CUSTOM',
          'display2': null,
          'description': 'There are some teams were assigned'});
      var cmbReportLimitAccess = model.getStore('reportLimitAccessStore');
      var groupPolicyId = model.get('form.id') | 0;
      var reportLimitAccess  = model.get('form.reportLimitAccess');
      console.log('model ', model);
      var assignedReportLimitAccessTeamStore = model.getStore('assignedReportLimitAccessTeamStore');
      assignedReportLimitAccessTeamStore.proxy.extraParams = {groupPolicyId: groupPolicyId};
      assignedReportLimitAccessTeamStore.load({
        scope: this,
        callback: function(records, operation, success) {
            if(records.length > 0){
                cmbReportLimitAccess.removeAll();
                cmbReportLimitAccess.add(customRecord);
                model.set('customReportLimitAccessFocusValue', 0);
            }else{
                cmbReportLimitAccess.proxy.extraParams = {lookupKey:'GROUP_POLICY_REPORT_ACCESS'};
                cmbReportLimitAccess.load(
                  function(recs, ope, suc){
                      if(reportLimitAccess){
                        model.set('customReportLimitAccessFocusValue', reportLimitAccess.id);
                      }
                  }
               );
            }
        }
      });

    },
    onWindowClosedHandler: function () {
        var me = this,
            w = me.getView().up();
        w.close();

    },
    getItemId: function (vm, prop) {
        if (!vm.get(prop)) {
            return null;
        } else {
            return vm.get(prop);
        }
    },
    getAssignTeamList: function(groupPolicyId){
        var me = this, model = me.getViewModel(),
        assignedTeamStore = model.getStore('assignedTeamStore');
        var teams = [];
        assignedTeamStore.data.items.forEach(function(item){
            teams.push({id:groupPolicyId, teamId:item.id});
        });
        return teams;
    },
    onFormSubmit: function () {
        var me = this;
        var ref = me.getRef();

        var form = ref.refs.formRef;

        if(form.isValid()){

            var id = me.getItemId(ref.vm, 'form.id');
            var status = me.getItemId(ref.vm, 'form.status');
            var limitAccess = me.getItemId(ref.vm, 'form.limitAccess');
            var newTicketNotify = me.getItemId(ref.vm, 'form.newTicketNotify');
            var assignedNotify = me.getItemId(ref.vm, 'form.assignedNotify');
            var replyNotify = me.getItemId(ref.vm, 'form.replyNotify');
            var changeStatusNotify = me.getItemId(ref.vm, 'form.changeStatusNotify');

            var assignTeamList = me.getAssignTeamList(id);
            var assignReportLimitAccessTeamList = me.getAssignReportAccessTeamList(id);
            var reportLimitAccess = me.getItemId(ref.vm, 'form.reportLimitAccess');

            var data = {
                'id':  id,
                'groupName': me.getItemId(ref.vm, 'form.groupName'),
                'createTicket': me.getItemId(ref.vm, 'form.createTicket'),
                'subTicket': me.getItemId(ref.vm, 'form.subTicket'),
                'editTicket': me.getItemId(ref.vm, 'form.editTicket'),
                'editRequestor': me.getItemId(ref.vm, 'form.editRequestor'),
                'postTicket': me.getItemId(ref.vm, 'form.postTicket'),
                'closeTicket': me.getItemId(ref.vm, 'form.closeTicket'),
                'assignTicket': me.getItemId(ref.vm, 'form.assignTicket'),
                'mergeTicket': me.getItemId(ref.vm, 'form.mergeTicket'),
                'deleteTicket': me.getItemId(ref.vm, 'form.deleteTicket'),
                'deptTransfer': me.getItemId(ref.vm, 'form.deptTransfer'),
                'changeStatus': me.getItemId(ref.vm, 'form.changeStatus'),
                'status': status.getData().display1,
                'limitAccess': limitAccess.getData().display1,
                'newTicketNotify': newTicketNotify.getData().display1,
                'assignedNotify': assignedNotify.getData().display1,
                'replyNotify': replyNotify.getData().display1,
                'changeStatusNotify': changeStatusNotify.getData().display1,
                'description': me.getItemId(ref.vm, 'form.description'),
                'reportAccess': reportLimitAccess.getData().display1,
                assignTeamList : assignTeamList,
                assignReportLimitAccessTeamList : assignReportLimitAccessTeamList
            };

            var view = me.getView();

            Ext.MessageBox.show({
                title: 'Confirmation',
                msg: "Are sure to Save group policy?",
                buttons: Ext.MessageBox.YESNO,
                scope: this,
                icon: Ext.MessageBox.QUESTION,
                fn: function (bt) {
                    if (bt == 'yes') {
                        me.doSave(data, function (record) {
                            if (view.cbFn) {
                                view.cbFn();
                            }
                        });
                    }
                }
            });
        }
    },
    doSave: function (data, cb) {
        var me = this;
        var view = me.getView();

        var model = Ext.create('Workflow.model.ticket.TicketGroupPolicy', data);
        if(model.id > 0){
            me.getView().mask("Data processing...");
            Ext.Ajax.request({
                url: 'api/ticket/setting/grouppolicy/update',
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                jsonData: data,
                success: function (response, operation) {
                    me.getView().unmask();
                    me.showToast("Group Policy was saved successfully.");
                    var record = Ext.decode(response.responseText);
                    cb(record);
                    me.onWindowClosedHandler();
                },
                failure: function (data, operation) {
                    me.getView().unmask();
                    me.showToast("Failed to save Group Policy.");
                    var response = Ext.decode(data.responseText);
                    Ext.MessageBox.show({
                        title: 'Error',
                        msg: response.msg,
                        buttons: Ext.MessageBox.OK,
                        icon: Ext.MessageBox.ERROR
                    });
                }
            });
        }else{
          model.save({
              params: data,
              failure: function (record, operation) {
                  // me.showToast(Ext.String.format('Failed to save group policy: {0}...', model.data.groupName));
                  // cb(record);
                  model.getProxy().on('exception', function (proxy, response, operation) {
                      var errors = Ext.JSON.decode(response.responseText).msg;
                      Ext.MessageBox.alert('Validation', errors);
                  }, this);
              },
              success: function (record, operation) {
                  me.showToast(Ext.String.format('Save successfuly group policy: {0}...',model.data.groupName));
                  me.onWindowClosedHandler();
                  cb(record);
              }

          });
      }
    },
    onAddCustomLimitAccessHandler: function(el, e, eOpts){
        var me = this;
        var vm = me.getView().getViewModel();
        var groupPolicyId = vm.get('form.id') | 0;
        var assignedTeamStore = vm.getStore('assignedTeamStore');
        var limitAccessUpdatedId = vm.get('limitAccessId');

        Ext.create('Workflow.view.ticket.setting.grouppolicy.AssignTeamForm', {
            assignedTeamStore: assignedTeamStore,
            groupPolicyId: groupPolicyId,
            cbFn: function (assignedTeamStore, formView) {
                vm.getStore('assignedTeamStore').setData(assignedTeamStore.getData());
                var cmbLimitAccess = vm.getStore('limitAccessStore');
                cmbLimitAccess.removeAll();
                if(assignedTeamStore.count() > 0){
                  var customRecord = Ext.create('Workflow.model.ticket.TicketLookup', {
                      'id': 0,
                      'display': 'Custom',
                      'display1': 'CUSTOM',
                      'display2': null,
                      'description': 'There are some teams were assigned'});
                    cmbLimitAccess.add(customRecord);
                    vm.set('customLimitAccessFocusValue', 0);
                }else{
                    cmbLimitAccess.proxy.extraParams = {lookupKey:'GROUP_POLICY_LIMIT_ACCESS'};
                    cmbLimitAccess.load(

                      function(recs, ope, suc){
                          vm.set('customLimitAccessFocusValue', limitAccessUpdatedId);
                      }

                  );
                }
                formView.close();
            }
        }).show();
    },
    onAddCustomReportLimitAccessHandler:  function(el, e, eOpts){
        var me = this;
        var vm = me.getView().getViewModel();
        var groupPolicyId = vm.get('form.id') | 0;
        var assignedReportLimitAccessTeamStore = vm.getStore('assignedReportLimitAccessTeamStore');
        var reportLimitAccessUpdatedId = vm.get('reportLimitAccess');

        Ext.create('Workflow.view.ticket.setting.grouppolicy.AssignTeamForm', {
            assignedTeamStore: assignedReportLimitAccessTeamStore,
            groupPolicyId: groupPolicyId,
            cbFn: function (assignedReportLimitAccessTeamStore, formView) {
                vm.getStore('assignedReportLimitAccessTeamStore').setData(assignedReportLimitAccessTeamStore.getData());
                var cmbReportLimitAccess = vm.getStore('reportLimitAccessStore');
                cmbReportLimitAccess.removeAll();
                if(assignedReportLimitAccessTeamStore.count() > 0){
                  var customRecord = Ext.create('Workflow.model.ticket.TicketLookup', {
                      'id': 0,
                      'display': 'Custom',
                      'display1': 'CUSTOM',
                      'display2': null,
                      'description': 'There are some teams were assigned'});
                    cmbReportLimitAccess.add(customRecord);
                    vm.set('customReportLimitAccessFocusValue', 0);
                }else{
                    cmbReportLimitAccess.proxy.extraParams = {lookupKey:'GROUP_POLICY_REPORT_ACCESS'};
                    cmbReportLimitAccess.load(

                      function(recs, ope, suc){
                          vm.set('customReportLimitAccessFocusValue', reportLimitAccessUpdatedId);
                      }

                  );
                }
                formView.close();
            }
        }).show();
    },
    showWindowDialog: function (lauchFromEl, windowClass, model, cb) {
        var content = Ext.create(windowClass, {
            viewModel: {
                data: model
            },
            cbFn: cb
        });
        var me = this;
        var w = new Ext.window.Window({
            rtl: false,
            modal: true,
            title: 'Assign Team Form',
            layout: 'fit',
            closable: true,
            //collapsable: true,
            items: content,
            height: me.getView().getHeight(),
            width: 970,
            doClose: function () {
                w.hide(lauchFromEl, function () {
                    w.destroy();
                });
            }
        });
        w.show(lauchFromEl);
    },
    getAssignReportAccessTeamList: function(groupPolicyId){
      var me = this, model = me.getViewModel(),
      assignedTeamStore = model.getStore('assignedReportLimitAccessTeamStore');
      var teams = [];
      assignedTeamStore.data.items.forEach(function(item){
          teams.push({id:groupPolicyId, teamId:item.id});
      });
      return teams;
    }
});
