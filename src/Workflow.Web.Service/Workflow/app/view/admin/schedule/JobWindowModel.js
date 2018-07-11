Ext.define('Workflow.view.admin.schedule.JobWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.schedule-jobwindow',
    data: {
        
    },
    formulas: {
        readOnlyField: function (get) {
            return false;
        }
    }
});
