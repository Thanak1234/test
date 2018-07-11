Ext.define('Workflow.view.common.fileUpload.FileUploadModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-fileupload-fileupload',
    data: {
        name        : 'Workflow',
        priority    : 1
    },

    stores: {
        dataStore: {
            model: 'Workflow.model.common.FileUpload'

        }
    },

    formulas: {

        canAddRemove: function (get) {
            if (get('viewSetting') ) {
                return !get('viewSetting').formUploadBlock.readOnly;
            } else {
                return false;
            }
        }
    }


});
