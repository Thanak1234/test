/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.pbf.FormViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.pbf-form',
    data: {
        projectBrief: Ext.create('Workflow.model.pbfForm.ProjectBrief').getData(),
        viewSetting : null
    },    
    formulas: {
        editable: function (get) {
            if (get('viewSetting') && get('viewSetting').projectBriefForm.readOnly) {
                return false;
            }else{
                return true;
            }
        },
        userRequest: function (get) {
            if (get('viewSetting') && get('viewSetting').projectBriefForm.userRequest) {
                return true;
            } else {
                return false;
            }
        },
        visibleNr: function (get) {
            if (get('viewSetting') && get('viewSetting').projectBriefForm.visibleNr) {
                return true;
            }else{
                return false;
            }
        },
        showTechnician: function (get) {
            if (get('viewSetting') && get('viewSetting').projectBriefForm.showTechnician) {
                return true;
            } else {
                return false;
            }
        },
        technicianReadOnly: function (get) {
            if (get('viewSetting') && get('viewSetting').projectBriefForm.technicianReadOnly) {
                return false;
            } else {
                return true;
            }
        }
    }
});
