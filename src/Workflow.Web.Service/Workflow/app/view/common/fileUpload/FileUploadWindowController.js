Ext.define('Workflow.view.common.fileUpload.FileUploadWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.common-fileupload-fileuploadwindow',
    sumbmitForm: function () {

        var me = this,
            view = me.getView(),
            item = view.getViewModel().get('item'),
            form = view.getReferences().form.getForm(),
            mainView = view.mainView.getView();

        item.processCode = mainView.processCode;
        if (form.isValid() && view.cbFn) {
            item.serial = mainView.guid; 
            form.submit({
                waitMsg: 'Uploading file...',
                headers: { 'Content-Type': 'multipart/form-data; charset=UTF-8' },
                params: item,
                success: function (response, request) {
                    // add item to grid
                    var attachment = request.result.data;
                    //console.log('attachment', attachment);
                    //if (!attachment) {
                    //    attachment = request.result.data[0];
                    //}
                    
                    attachment.isTemp = true;
                    view.cbFn(Ext.create('Workflow.model.common.FileUpload', attachment));

                    form.reset();
                },
                failure: function (response, request) {
                    var hasResponseText = (request.response.responseText || request.response.responseText != '')
                    var responseObj = { Message: null };
                    try {
                        if (hasResponseText) {
                            responseObj = Ext.JSON.decode(request.response.responseText);
                        }
                    } catch (err) {
                        responseObj.Message = 'File is too large. File must be less than 50 megabytes.';
                    }
                    
                    if (hasResponseText) {
                        alert(responseObj.Message);
                    }
                }
            });
        }
    }
     
});
