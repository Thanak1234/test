Ext.define('Workflow.view.admsr.AdmsrModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.admsr',
    data: {
        information: null,
        viewSetting: null
    },
    formulas: {
        config: function (get) {
            return get('viewSetting').container;
        }
    }
});
