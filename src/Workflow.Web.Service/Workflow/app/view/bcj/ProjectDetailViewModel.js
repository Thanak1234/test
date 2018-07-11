/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.bcj.ProjectDetailViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.bcj-projectdetailview',
    data: {
        projectDetail: Ext.create('Workflow.model.bcjForm.ProjectDetail').getData(),
        viewSetting : null
    },
    formulas:{
        editable: function (get) {
            if (get('viewSetting') && get('viewSetting').projectDetail.readOnly) {
                return false;
            }else{
                return true;
            }
        },
        projectItemEditable: function (get) {
            if (get('viewSetting') && get('viewSetting').projectItem.readOnly) {
                return false;
            } else {
                return true;
            }
        }
    }
});
