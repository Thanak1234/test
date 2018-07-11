/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.hr.erf.FormViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.erf-form',
    data: {
        requisition: Ext.create('Workflow.model.erfForm.Requisition').getData(),
        viewSetting : null
    },    
    formulas: {
        editable: function (get) {
            if (get('viewSetting') && get('viewSetting').requisitionForm.readOnly) {
                return false;
            }else{
                return true;
            }
        },
        showRefNo: function (get) {
            console.log('show', get('viewSetting') && get('viewSetting').requisitionForm.showRefNo);
            if (get('viewSetting') && get('viewSetting').requisitionForm.showRefNo) {
                return true;
            }else{
                return false;
            }
        }
    }
});
