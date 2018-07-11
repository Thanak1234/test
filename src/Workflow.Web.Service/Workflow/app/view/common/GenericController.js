Ext.define('Workflow.view.common.GenericController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.genericform',
    init: function() {
        var me = this;
        var view = me.getView();
        if (!Ext.isEmpty(view.actionUrl)) {
            me.actionUrl = view.actionUrl;
        }
    },
    renderSubForm: function (data) {
        var me = this,
            view = me.getView();
        var viewmodel = view.getViewModel();
        viewmodel.setData(data);
        if (Ext.isFunction(view.loadData)) {
            view.loadData(viewmodel);
        }
    },
    clearData: function () {
        var view = this.getView();
        if (Ext.isFunction(view.clearData)) {
            view.clearData();
        }
    },
    validateForm: function (data) {
        var me = this;
        var view = me.getView();
        if (Ext.isFunction(view.validate)) {
            return view.validate(data);
        }
    },
    getRequestItem: function () {
        var me = this, view = me.getView();
        var viewmodel = view.getViewModel();
        if (Ext.isFunction(view.transformData)) {
            view.transformData(viewmodel);
        }
        var data = viewmodel.getData(true);
        if (data.viewSetting) {
            data.viewSetting = null;
        }
        me.removeStores(data);
        me.excludeProperties(data);

        return data;
    },
    removeStores: function (data) {
        var me = this;
        Ext.iterate(data, function (prop, v) {
            if (me.isStore(data[prop]))
                delete data[prop];
        });
    },
    isStore: function(store){
        return store && (store instanceof Ext.data.Store);
    },
    excludeProperties: function (data) {
        var me = this;
        var view = me.getView();
        var excludeProps = view.excludeProps;
        if (excludeProps && excludeProps.length > 0) {
            Ext.iterate(data, function (prop, v) {
                if (me.isContain(excludeProps, prop))
                    delete data[prop];
            });
        }
    },
    isContain: function (props, prop) {
        var contain = false;
        props.forEach(function (v, i) {
            if (v.toLowerCase() == prop.toLowerCase()) {
                contain = true;
                return;
            }
        });
        return contain
    }
});
