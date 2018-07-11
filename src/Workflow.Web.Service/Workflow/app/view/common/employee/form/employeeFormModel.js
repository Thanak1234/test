Ext.define('Workflow.view.common.employee.form.EmployeeFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-employee-form-employeeform',
    requires: [
        'Workflow.model.common.Employee'
    ],

    data: {
        action: 'ADD',
        mainView: null,
        employeeInfo: null,
        formTitle: 'Add Employee',
        employee: null,
        team: null,
        reportTo: null
    },
    stores :{
        departments: Ext.create('Workflow.store.department.Department').load({ params: { query: '' } })
    },
    formulas: {
        submitBtText: function (get) {
            var textLabel = 'Submit', action = get('action');
            if (action === 'ADD') {
                textLabel = 'Save';
            } else if (action === 'EDIT') {
                textLabel = 'Update';
            }
            return textLabel;
        }
        //,
        //concateDisplayName: function (get) {
        //    var emp = get('employeeInfo');
        //    return emp.data.lastName + ' ' + emp.data.firstName;
        //}

    },
    submitBtVisible: function (get) {
        var action = get('action');
        return action !== 'View'

    },
    bindToRequestor: function (employeeModel) {
        var me = this,
            mainView = me.data.mainView,
            requestorViewModel = mainView.getViewModel();

        var team = me.get('team');
        var employeeInfo = requestorViewModel.get('employeeInfo');
        if (!employeeInfo) {
            employeeInfo = new Ext.create('Workflow.model.common.EmployeeInfo');
        }
        
        employeeInfo.set('id', employeeModel.Id);
        employeeInfo.set('employeeNo', employeeModel.EmpNo);
        employeeInfo.set('fullName', employeeModel.DisplayName);
        employeeInfo.set('position', employeeModel.JobTitle);
        employeeInfo.set('phone', employeeModel.MobilePhone);
        employeeInfo.set('ext', employeeModel.Telephone);
        employeeInfo.set('reportTo', employeeModel.ReportTo);
        employeeInfo.set('empType',  'MANUAL');
        

        if (team) {
            employeeInfo.set('subDept', team.get('deptName'));
            employeeInfo.set('groupName', team.get('groupName'));
            employeeInfo.set('devision', team.get('deptType'));
        }
        requestorViewModel.set('employeeInfo', employeeInfo);
    }

});
