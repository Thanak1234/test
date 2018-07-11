Ext.define('Workflow.view.common.fileUpload.SimpleFileUploadDialogController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.simple-file-upload-dialog',

    initComponent: function () {

    },

    sumbmitForm: function () {

        var me = this,
            view = me.getView(),
            form = view.getReferences().form.getForm();
        if (form.isValid()) {
            form.submit({
                url: Workflow.global.Config.baseUrl + 'api/forms/attach',
                method: 'POST',
                waitMsg: 'Please wait while file is uploading...',
                success: function (fp, o) {
                    view.cbFn(o.result);
                    me.showToast("<b>" + o.result.fileName + "<b> was added...")

                },
                failure: function (opt, operation) {
                    me.showToast("Failed to add the file...");
                }
            });
        }
        
    }
});