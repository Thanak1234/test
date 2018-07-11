Ext.define('Workflow.view.icd.employeeList.requestEmployeeModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.icd-employeelist-requestemployee',
    data:{
        selectedItem: null,
        viewSetting: null
    }
    ,
    stores: {
        userStore: {
            model: 'Workflow.model.itRequestForm.RequestUser'
        }
    },
    formulas: {

        canAddRemove: function (get) {
            
            //debugger;
            var vs = get('viewSetting');
            var vsa = get('viewSetting').incidentEmployeeBlock.addEdit;

            if (get('viewSetting') && get('viewSetting').incidentEmployeeBlock.addEdit) {
                return true;
            } else {
                return false;
            }
        },

        editable: function (get) {
            if (get('selectedItem') && get('viewSetting') && get('viewSetting').incidentEmployeeBlock.addEdit) {
                return true;
            } else {
                return false;
            }

        },

        canView: function (get) {
            if (get('selectedItem')) {
                return true;
            } else {
                return false;
            }

        }
    }

});