Ext.define('Workflow.view.irf.IRFModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.irf',
    data: {
        formData: null,
        viewSetting: null
    },
    formulas: {
        config: function (get) {
            return get('viewSetting').container;
        }
    }
});
