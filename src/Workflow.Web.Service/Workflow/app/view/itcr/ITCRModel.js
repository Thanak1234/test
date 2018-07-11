Ext.define('Workflow.view.itcr.ITCRModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.itcr',
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
