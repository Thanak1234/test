Ext.define('Workflow.view.common.employee.form.EmployeeFormController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.common-employee-form-employeeform',
    requires: [
        'Workflow.model.common.Employee'
    ],
    submitForm: function (button, e, options) {
        var me = this,
            view = me.getView(),
        form = view.down('form');

        if (form.isValid()) {
            var model = form.getRecord(),
                values = form.getValues(),
                emp = me.getViewModel().get('employee');

            var empid = 0;
            try {
                empid = emp.getId();
            } catch (e) {
                console.log(e);
            }
            if (empid > 0) {
                model = Ext.create('Workflow.model.common.Employee', emp.data);
                model.set('Department', null);
            } else {
                if(!isNaN(parseInt(values.EmpNo)) && values.EmpNo.length < 6){
                    values.EmpNo = (('000000' + values.EmpNo).substr(-6));
                }
                
                model = Ext.create('Workflow.model.common.Employee');
                model.set('LoginName', values.EmpNo);
                model.set('EmpType', 'MANUAL');
                model.set('Active', 1);
                model.setId(0);
            }
            model.set(values);
            model.save({
                callback: function (record, operation, success) {
                    var msg = null, msgStatus = null;
                    
                    if (operation.wasSuccessful()) {
                        var responseObj = Ext.JSON.decode(operation._response.responseText);
                        if (responseObj.status==1) {
                            me.getViewModel().bindToRequestor(responseObj.obj);
                            msg = 'Your data was saved successfully!';
                            msgStatus = 'Done';
                            Ext.destroy(me.getView());
                        } else if (responseObj.status == 2) {
                            msg = 'This requestor was already existed...';
                            msgStatus = 'Failed';
                        } else if (responseObj.status == 0) {
                            msg = responseObj.message;
                            msgStatus = 'Failed';

                        }                       

                    } else {
                        msg = 'Save errored, please try again...'; 
                        msgStatus = 'Failed';

                    }

                    me.showToast(msg, msgStatus);

                }

            });
        }  
    },
    showToast: function (s, title) {
        Ext.toast({
            html: s,
            closable: false,
            align: 't',
            slideInDuration: 400,
            minWidth: 400
        });
    }

});
