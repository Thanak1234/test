Ext.define('Workflow.view.events.common.FormBaseModel', {
    extend: 'Workflow.view.RequestFormModel',
    alias: 'viewmodel.formbase',
    data: {
        name: 'workflow',
        employeeInfo: null,
        formRequestData: null,
        viewSetting: null
    },
    formulas: {
        readOnly: function (get) {

            if (get('viewSetting') && get('viewSetting').containerBlock.readOnly) {
                return true
            } else {
                return false;
            }
        }
    }
});
