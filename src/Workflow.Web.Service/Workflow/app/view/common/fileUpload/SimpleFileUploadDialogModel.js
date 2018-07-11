Ext.define('Workflow.view.common.fileUpload.SimpleFileUploadDialogModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.simple-file-upload-dialog',
    
    formulas: {
        submitBtText: function (get) {
            return 'Add';
        },
        btnSubmitIconCls: function (get) {
            return 'fa fa-upload';
        },
        btnCloseIconCls: function (get) {
            return 'fa fa-times-circle-o';
        }
    }

});