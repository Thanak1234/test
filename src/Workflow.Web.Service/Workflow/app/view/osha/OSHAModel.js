Ext.define('Workflow.view.osha.OSHAModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.osha',
    data: {
        viewSetting: null,
        information: null
    },
    formulas: {
        config: function (get) {
            return get('viewSetting').container;
        }
    }
});
