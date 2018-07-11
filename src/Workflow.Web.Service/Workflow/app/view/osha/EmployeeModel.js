Ext.define('Workflow.view.osha.EmployeeModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.osha-employee',
    data: {
        selectedItem: null,
        config: null
    },
    stores : {
        userStore : {
            model: 'Workflow.model.osha.RequestUser'
        }
    },
    formulas: {

    }
});
