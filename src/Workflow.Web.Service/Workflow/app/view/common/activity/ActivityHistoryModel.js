/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.common.activity.ActivityHistoryModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-activity-activityhistory',
    data: {
        selectedItem : null 
    },
    stores : {
        dataStore : {
             model : 'Workflow.model.common.ActivityHistory'            
        }
    }
});
