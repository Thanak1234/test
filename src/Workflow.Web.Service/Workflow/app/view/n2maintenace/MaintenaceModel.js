Ext.define('Workflow.view.n2maintenace.MaintenaceModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.n2maintenace',
    data: {
        information: {
            mode: null,
            requestType: null,
            referenceNumber: null,
            location: null,
            subLocation: null,
            ccdId: null,
            remark: null,
            wrjd: null,
            instruction: null,
            jaDate: null,
            jaTechnician: null,
            workType: null,
            tcDesc: null
        },
        viewSetting: null
    },
    stores: {
    },
    formulas: {
        config: function (get) {
            return get('viewSetting').container;
        }
    }
});
