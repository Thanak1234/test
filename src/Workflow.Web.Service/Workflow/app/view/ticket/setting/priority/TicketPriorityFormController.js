Ext.define('Workflow.view.ticket.setting.priority.TicketPriorityFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-priority-ticketpriorityform',

    init: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();

        var ticketSettingSlaStore = model.getStore('ticketSettingSlaStore');
        ticketSettingSlaStore.proxy.extraParams= {query: null};
        ticketSettingSlaStore.load();

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
            var sla = me.getItemId(ref.vm, 'form.sla');
            var data = {
                'id':  id,
                'priorityName': me.getItemId(ref.vm, 'form.priorityName'),
                'description': me.getItemId(ref.vm, 'form.description'),
                'slaId': sla.getId()
            };

            var view = me.getView();

            Ext.MessageBox.show({
                title: 'Confirmation',
                msg: "Are sure to Save Priority?",
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

        var model = Ext.create('Workflow.model.ticket.TicketPriority', data);
        model.save({
            params: data,
            failure: function (record, operation) {
                // me.showToast(Ext.String.format('Failed to save priority: {0}...', model.data.priorityName));
                // cb(record);
                model.getProxy().on('exception', function (proxy, response, operation) {
                    var errors = Ext.JSON.decode(response.responseText).msg;
                    Ext.MessageBox.alert('Validation', errors);
                }, this);
            },
            success: function (record, operation) {
                me.showToast(Ext.String.format('Save successfuly priority: {0}...', model.data.priorityName));
                cb(record);
            }

        });
    }
});
