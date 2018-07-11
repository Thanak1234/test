Ext.define('Workflow.view.ticket.setting.item.TicketItemFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-item-ticketitemform',

    init: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();

        var status = model.getStore('statusStore');
        status.proxy.extraParams = {lookupKey:'ITEM_STATUS'};
        status.load({
            scope: this,
            callback: function(records, operation, success) {
                console.log('view ', view, ' aksdfjadsf ', records);
            }
        });

        model.getStore('ticketTeamStore').load();
        var ticketSubCateStore = model.getStore('ticketSubCateStore');
        ticketSubCateStore.getProxy().extraParams = { cateId: 0, breadcrumb: true };
        ticketSubCateStore.load();
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
            var team = me.getItemId(ref.vm, 'form.team');
            var subCate = me.getItemId(ref.vm, 'form.subCate');
            var status = me.getItemId(ref.vm, 'form.status');

            var data = {
                'id':  id,
                'subCateId': subCate.getId(),
                'itemName': me.getItemId(ref.vm, 'form.itemName'),
                'teamId': team.getId(),
                'slaId': me.getItemId(ref.vm, 'form.slaId'),
                'description': me.getItemId(ref.vm, 'form.description'),
                'status': status.getData().display1
            };

            var view = me.getView();

            Ext.MessageBox.show({
                title: 'Confirmation',
                msg: "Are sure to Save Item?",
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

        var itemModel = Ext.create('Workflow.model.ticket.TicketItem', data);
        itemModel.save({
            params: data,
            failure: function (record, operation) {
                // me.showToast(Ext.String.format('Failed to save Item: {0}...', itemModel.data.itemName));
                // cb(record);
                itemModel.getProxy().on('exception', function (proxy, response, operation) {
                    var errors = Ext.JSON.decode(response.responseText).msg;
                    Ext.MessageBox.alert('Validation', errors);
                }, this);
            },
            success: function (record, operation) {
                me.showToast(Ext.String.format('Save successfuly Item: {0}...',itemModel.data.itemName));
                cb(record);
            }

        });
    }
});
