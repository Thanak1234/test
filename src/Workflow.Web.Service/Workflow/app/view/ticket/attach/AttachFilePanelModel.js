/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.attach.AttachFilePanelModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.ticket-attach-attachfile',
    data: {
        name: 'Workflow'
    },
    stores: {
        attchedFilesStore: {
            model: 'Workflow.model.common.SimpleFileUpload'
        }
    }
});
