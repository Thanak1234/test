/**
 * 
 *Author : Phanny 
 */
Ext.define('Workflow.view.common.requestor.RequestorModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-requestor-requestor',
    data: {
        employeeInfo    : null,
        priorityVal: 1, //Ext.create('Workflow.model.common.Priorities', { id: 1 }),
        disableEditButton: true
    },
    formulas:{
        clearRequestor : function(get) {
            // if(get('employeeInfo')){
            //     return false;
            // }else{
            //     return true;
            // }
            
            return false;
        },


   

        readOnly: function (get) {

            if (get('viewSetting') && get('viewSetting').requestorFormBlock.readOnly) {
                return true
            } else {
                return false;
            }
        },
        

        //enable button edit when employee selected is Manual
        toggleButtonEdit: function (get) {
            var emp = get('employeeInfo');
            if (emp) {
                if(emp.data.empType=='MANUAL'){
                    return false;
                }
            }
            return true;
        }
    }

});
