/**
 * 
 *Author : Phanny 
 */
Ext.define('Workflow.view.RequestFormModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.requestform',
    data: {
        serial: null,
        activity: 'Submission',
        formData: null , //No Used
        requestHeaderId: 0,
        requestorId: 0
    },
    stores: {
        activityConfig: {
            model: 'Workflow.model.ActivityConfig',
            autoLoad: false,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/forms/activities-config',
                reader: {
                    type: 'json'
                }
            }
        }
    }
});
