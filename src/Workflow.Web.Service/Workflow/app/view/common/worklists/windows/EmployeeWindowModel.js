Ext.define('Workflow.view.common.worklists.EmployeeWindowModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-worklists-employeewindow',
    data: {
        employeeInfo: null,
        comment: null
    },
    stores: {
        employees: {
            type: 'store',
            model: 'Workflow.model.common.Employee',
            autoLoad: true
        }
    }

});
