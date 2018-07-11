Ext.define('Workflow.view.ticket.setting.subcategory.TicketSubCategoryFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-subcategory-ticketsubcategoryform',

    init: function () {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getView().getReferences();

        var status = model.getStore('statusStore');
        status.proxy.extraParams = {lookupKey:'SUBCATEGORY_STATUS'};
        status.load();

        model.getStore('ticketCateStore').load();
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
            var cate = me.getItemId(ref.vm, 'form.cate');
            var status = me.getItemId(ref.vm, 'form.status');

            var data = {
                'id':  id,
                'cateId': cate.getId(),
                'subCateName': me.getItemId(ref.vm, 'form.subCateName'),
                'description': me.getItemId(ref.vm, 'form.description'),
                'status': status.getData().display1
            };

            var view = me.getView();

            Ext.MessageBox.show({
                title: 'Confirmation',
                msg: "Are sure to Save sub category?",
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

        var model = Ext.create('Workflow.model.ticket.TicketSettingSubCategory', data);
        model.save({
            params: data,
            failure: function (record, operation) {
                // me.showToast(Ext.String.format('Failed to save sub category: {0}...', model.data.subCateName));
                // cb(record);
                model.getProxy().on('exception', function (proxy, response, operation) {
                    var errors = Ext.JSON.decode(response.responseText).msg;
                    Ext.MessageBox.alert('Validation', errors);
                }, this);
            },
            success: function (record, operation) {
                me.showToast(Ext.String.format('Save successfuly sub category: {0}...', model.data.subCateName));
                cb(record);
            }

        });
    }
});
