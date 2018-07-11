Ext.define('Workflow.view.FormComponentController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.form-component',
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear: 'clearData'
            }
        }
    },
    loadData: function (data, viewSetting) {
        var me = this,
            view = me.getView(),
            model = me.getView().getViewModel();

        // TODO SOMTHING
        
        view.getForm().reset();
        view.loadData(data, viewSetting);
        model.set('viewSetting', viewSetting);
    },
    clearData: function () {
        this.getView().reset();
    }
});