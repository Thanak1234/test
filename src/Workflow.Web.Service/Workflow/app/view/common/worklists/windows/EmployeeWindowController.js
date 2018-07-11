Ext.define('Workflow.view.common.worklists.EmployeeWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.common-worklists-employeewindow',    
    onEmployeeChanged: function (el, newValue, oldValue, eOpts) {
        if (newValue && newValue.trim()) {
            el.getTrigger('clear').show();
            this.setViewData(el.getSelection());
        } else {
            
            el.getTrigger('clear').hide();
            this.getView().getViewModel().set('employeeInfo', null);
        }  
        
    },
    
    onEmployeeCleared: function(el){
        el.clearValue( );
    },
    
    setViewData: function(emp){
        this.getView().getViewModel().set('employeeInfo', emp);
    },
    onAddClickHandler: function () {
        var me = this;
        var info = me.getView().getViewModel().get('employeeInfo');
        var main = me.getView().mainView;
        var self = me.getView();
        var vm = self.getViewModel();

        if (main) {
            var destination = me.getDestination(info);
            var comment = vm.get('comment');
            if((!comment || comment == '') && self.showComment){
                Ext.MessageBox.alert('Require', 'Please input comment before you redirect.');
            }else{
                if (main.addDestination(destination, self.payload, comment)) {
                    me.closeWindow();
                }            
            }
        }
    },
    getDestination: function (record) {

        if (record == null)
            return null;

        var emp = record.getData();

        var employee = Ext.create('Workflow.model.common.worklists.Destination');

        employee.set('LoginName', emp.loginName);
        employee.set('EmpNo', emp.employeeNo);
        employee.set('DisplayName', emp.fullName);
        employee.set('Position', emp.position);
        employee.set('Email', emp.email);
        employee.set('Telephone', emp.ext);
        employee.set('MobilePhone', emp.phone);
        employee.set('Manager', emp.hod);
        employee.set('GroupName', emp.groupName);
        employee.set('TeamName', emp.subDept);
        employee.set('DeptType', emp.deptType);

        return employee;
    }
});

