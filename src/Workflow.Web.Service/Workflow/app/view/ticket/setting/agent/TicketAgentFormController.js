Ext.define('Workflow.view.ticket.setting.agent.TicketAgentFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-agent-ticketagentform',
    init: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();

        //model.getStore('accountTypeStore').load();
        model.getStore('statusStore').load();
        model.getStore('groupPolicyStore').load();
        model.getStore('departmentStore').load();
        var  ticketSettingAgentTeamsStore = model.getStore('ticketSettingAgentTeamsStore');
        ticketSettingAgentTeamsStore.proxy.extraParams = {agentId: 0};
        ticketSettingAgentTeamsStore.load();

    },
    onWindowClosedHandler: function () {
        var me = this,
            w = me.getView().up();
        w.close();

    },
    getAgentId: function (vm, prop) {
        if (!vm.get(prop)) {
            return null;
        } else {
            return vm.get(prop);
        }
    },
    onFormSubmit: function () {
        var me = this;
        var ref = me.getRef();

        var form = ref.refs.formRef;


            var id = me.getAgentId(ref.vm, 'form.id');
            var employee = me.getAgentId(ref.vm, 'form.employee');
            // var accountType = me.getAgentId(ref.vm, 'form.accountType');
            var status = me.getAgentId(ref.vm, 'form.status');
            var groupPolicy = me.getAgentId(ref.vm, 'form.groupPolicy');
            var department = me.getAgentId(ref.vm, 'form.dept');

            var data = {
                'id':  id,
                'empId': employee.getId(),
                'accountType': 'SYS_USER',
                'status': status.getData().display1,
                'groupPolicyId': groupPolicy.getId(),
                'deptId': department.getId(),
                'description': me.getAgentId(ref.vm, 'form.description')
            };

            var view = me.getView();
            Ext.MessageBox.show({
                title: 'Confirmation',
                msg: "Are sure to Save agent?",
                buttons: Ext.MessageBox.YESNO,
                scope: this,
                icon: Ext.MessageBox.QUESTION,
                fn: function (bt) {
                    if (bt == 'yes') {
                        me.doSave(data, function (record) {
                            if (view.cbFn) {
                                view.cbFn();
                            }
                            me.onWindowClosedHandler();
                        });
                    }
                }
            });

    },
    doSave: function (data, cb) {
        var me = this;
        var view = me.getView();

        var model = Ext.create('Workflow.model.ticket.TicketSettingAgent', data);
        model.save({
            params: data,
            failure: function (record, operation) {                
                model.getProxy().on('exception', function (proxy, response, operation) {
                    var errors = Ext.JSON.decode(response.responseText).msg;
                    Ext.MessageBox.alert('Validation', errors);
                }, this);
            },
            success: function (record, operation) {
                me.showToast('Save agent successfuly...');
                cb(record);
            }
        });

    }
});
