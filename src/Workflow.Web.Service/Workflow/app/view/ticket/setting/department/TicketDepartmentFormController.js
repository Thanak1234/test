Ext.define('Workflow.view.ticket.setting.department.TicketDepartmentFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-department-ticketdepartmentform',

    init: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();

        var status = model.getStore('statusStore');
        status.proxy.extraParams = {lookupKey:'DEPARTMENT_STATUS'};
        status.load();
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
    onFormSubmit: function () {
        var me = this;
        var ref = me.getRef();

        var form = ref.refs.formRef;

        if(form.isValid()){

            var id = me.getItemId(ref.vm, 'form.id');
            var defaultItem = me.getItemId(ref.vm, 'form.item');
            var status = me.getItemId(ref.vm, 'form.status');
            var data = {
                'id':  id,
                'deptName': me.getItemId(ref.vm, 'form.deptName'),
                'automationEmail': me.getItemId(ref.vm, 'form.automationEmail'),
                'description': me.getItemId(ref.vm, 'form.description'),
                'defaultItemId': defaultItem.getId(),
                'status': status.getData().display1
            };

            var view = me.getView();

            Ext.MessageBox.show({
                title: 'Confirmation',
                msg: "Are sure to Save Department?",
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
        }
    },
    doSave: function (data, cb) {
        var me = this;
        var view = me.getView();

        var model = Ext.create('Workflow.model.ticket.TicketDepartment', data);
        model.save({
            params: data,
            failure: function (record, operation) {
                // me.showToast(Ext.String.format('Failed to save Department: {0}...', model.data.deptName));
                // cb(record);
                model.getProxy().on('exception', function (proxy, response, operation) {
                    var errors = Ext.JSON.decode(response.responseText).msg;
                    Ext.MessageBox.alert('Validation', errors);
                }, this);
            },
            success: function (record, operation) {
                me.showToast(Ext.String.format('Save successfuly Department: {0}...',model.data.deptName));
                cb(record);
            }

        });
    }
});
