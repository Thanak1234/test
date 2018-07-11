Ext.define('Workflow.view.itad.ITAdModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.itad',
    data: {
        employee: null,
        viewSetting: null
    },
    formulas: {
        config: function (get) {
            return get('viewSetting').container;
        }
    }
});
